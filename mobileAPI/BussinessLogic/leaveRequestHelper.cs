using mobileAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mobileAPI.BussinessLogic
{
    public class leaveRequestHelper
    {
        public List<BiometricAttendance> GetAttendanceList(string employeecode, DateTime? fromdate, DateTime? todate)
        {
            try
            {
                fromdate = DateTime.Now;
                todate = DateTime.Now;

                using (MobileContext context = new MobileContext())
                {
                    return context.BiometricAttendance.AsEnumerable()
                                  .Where(x => x.EmpCode == employeecode
                                          && x.Date.IsDateBetweenDates(fromdate, todate)
                                         )
                                  .ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<LeaveApplDetails> GetLeaveApplDetailsForRecomand(string employeecode)
        {
            try
            {

                using (MobileContext context = new MobileContext())
                {
                    return (from emp in context.Employees.ToList()
                            join lvapl in context.LeaveApplDetails.ToList()
                              on emp.Code equals lvapl.EmpCode
                            where emp.RecommendedBy == employeecode
                               && lvapl.Status == STATUS.NEW.ToString()
                                && lvapl.AddDate.Value.Year == DateTime.Today.Year
                            select lvapl).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<LeaveApplDetails> GetLeaveApplDetailsList(string employeecode)
        {
            try
            {
                using (MobileContext context = new MobileContext())
                {
                    return context.LeaveApplDetails
                                  .AsEnumerable()
                                  .Where(x => x.EmpCode.Trim() ==employeecode .Trim()
                                           && x.AddDate.Value.Year == DateTime.Today.Year)
                                  .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<LeaveApplDetails> GetLeaveApplDetailsForApproval(string employeecode)
        {
            try
            {
                List<LeaveApplDetails> _leaveApplyDtls = new List<LeaveApplDetails>();
                using (MobileContext context = new MobileContext())
                {
                   var  _leaveApplyDtlsList = (from emp in context.Employees
                                             join lvaply in context.LeaveApplDetails
                                               on emp.Code equals lvaply.EmpCode
                                             where emp.ApprovedBy == employeecode
                                                || emp.RecommendedBy == employeecode
                                                // && lvaply.AddDate.Value.Year == DateTime.Today.Year
                                                && lvaply.Status != STATUS.CANCEL.ToString() && lvaply.Status != STATUS.APPROVED.ToString()
                                       select lvaply).ToList();

                    _leaveApplyDtlsList = _leaveApplyDtlsList.Where(lvaply=> lvaply.Status != STATUS.CANCEL.ToString() && lvaply.Status != STATUS.APPROVED.ToString()).ToList();
                    // is two stage approval
                    foreach (var lvapldtl in _leaveApplyDtlsList)
                    {
                        var employeeCodes = context.Employees.Where(emp => emp.Code == lvapldtl.EmpCode).FirstOrDefault();

                        //check is two stage 
                        if(employeeCodes.RecommendedBy != null)
                        {
                            // check is emmployee id is in Approve id
                            if(employeeCodes.ApprovedBy == employeecode)
                            {
                                if(lvapldtl.RecommendedId != null)
                                {
                                    _leaveApplyDtls.Add(lvapldtl);
                                }
                            }
                            else if (employeeCodes.RecommendedBy == employeecode)
                            {
                                if(lvapldtl.Status == STATUS.NEW.ToString())
                                _leaveApplyDtls.Add(lvapldtl);
                            }
                        }
                        else if(employeeCodes.ApprovedBy == employeecode)
                        {
                            _leaveApplyDtls.Add(lvapldtl);
                        }
                    }
                }

                return _leaveApplyDtls.Distinct().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public List<LeaveApplDetails> RecomandationProcess(string employeecode, List<LeaveApplDetails> leaveappldetails)
        {
            try
            {
                List<LeaveApplDetails> prcoessemployess = new List<LeaveApplDetails>();
                using (MobileContext context = new MobileContext())
                {
                    foreach (var lvaply in leaveappldetails)
                    {
                        try
                        {
                            if (lvaply.Status == STATUS.CANCEL.ToString())
                            {
                                updateusedleaves(lvaply.EmpCode, lvaply.LeaveDays, context);
                            }
                            //lvaply.status = status.inprogress.tostring();
                            lvaply.RecommendedId = employeecode;
                            lvaply.RecommendedName = getemployee(lvaply.RecommendedId)?.Name;
                            context.LeaveApplDetails.Update(lvaply);
                            if (context.SaveChanges() > 0)
                                prcoessemployess.Add(lvaply);
                        }
                        catch { }
                    }
                }

                return prcoessemployess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<LeaveApplDetails> LeaveApprovalProcess(string employeecode, List<LeaveApplDetails> leaveappldetails)
        {
            try
            {
                List<LeaveApplDetails> prcoessemployess = new List<LeaveApplDetails>();
                LeaveBalanceMaster _leavebalancemaster = null;
                using (MobileContext context = new MobileContext())
                { 
                    foreach (var lvaply in leaveappldetails)
                    {
                       
                        try
                        {
                            var _employee = context.Employees.Where(x => x.Code == lvaply.EmpCode).FirstOrDefault();
                            if (_employee.RecommendedBy?.Trim() == employeecode.Trim())
                            {
                               // lvaply.Status = STATUS.INPROGRESS.ToString();
                                lvaply.RecommendedId = employeecode;
                                lvaply.RecommendedName = getemployee(lvaply.RecommendedId)?.Name;
                            }

                            if (_employee.ApprovedBy?.Trim() == employeecode.Trim())
                            {
                                //lvaply.Status = STATUS.APPROVED.ToString();
                                //lvaply.ApprovedDate = DateTime.Now;
                                lvaply.ApprovedId = employeecode;
                                lvaply.ApprovedName = getemployee(lvaply.ApprovedId)?.Name;
                            }

                            
                            if (lvaply.Status.Equals(STATUS.CANCEL.ToString(),StringComparison.OrdinalIgnoreCase))
                            {
                                updateusedleaves(lvaply.EmpCode, lvaply.LeaveDays, context,true);
                                context.LeaveApplDetails.Update(lvaply);
                                context.SaveChanges();
                                prcoessemployess.Add(lvaply);
                                continue;
                            }
                            
                            // if (_employee != null)
                            // {
                            if (_employee.RecommendedBy?.Trim() == employeecode.Trim())
                            {
                                lvaply.Status = STATUS.INPROGRESS.ToString();
                                //lvaply.RecommendedId = employeecode;
                                //lvaply.RecommendedName = getemployee(lvaply.RecommendedId)?.Name;
                                //context.LeaveApplDetails.Update(lvaply);
                            }

                            if (_employee.ApprovedBy?.Trim() == employeecode.Trim())
                            {
                                lvaply.Status = STATUS.APPROVED.ToString();
                                lvaply.ApprovedDate = DateTime.Now;
                                //context.LeaveApplDetails.Update(lvaply);

                            }
                            context.LeaveApplDetails.Update(lvaply);
                            context.SaveChanges();
                            prcoessemployess.Add(lvaply);
                            //}
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }

                return prcoessemployess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public LeaveApplDetails InsertLeaveApplDetails(LeaveApplDetails leaveappldetails, out RETIRNSTATUS status, out string errormsg)
        {
            try
            {
                //if (!validateleaves(leaveappldetails.leavecode, leaveappldetails.empcode, leaveappldetails.leavefrom, leaveappldetails.leaveto, out errormsg))
                if (!validateleaves(leaveappldetails.LeaveCode, leaveappldetails.EmpCode, Convert.ToDecimal(leaveappldetails.LeaveDays), out errormsg))
                {
                    status = RETIRNSTATUS.FAIL;
                    return null;
                }

                using (MobileContext context = new MobileContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            // leaveappldetails.leavedays = calculatenoofdays(leaveappldetails.leavefrom, leaveappldetails.leaveto, leaveappldetails.halfdayfrom, leaveappldetails.halfdayto);
                            Employees employeeobj = getemployee(leaveappldetails.EmpCode);
                            leaveappldetails.EmpName = employeeobj.Name;
                            //leaveappldetails.RecommendedId = employeeobj.RecommendedBy;
                            //leaveappldetails.ApprovedId = employeeobj.ApprovedBy;
                            //leaveappldetails.ApprovedName = getemployee(leaveappldetails.ApprovedId)?.Name;
                            //leaveappldetails.RecommendedName = getemployee(leaveappldetails.RecommendedId)?.Name;

                            updateusedleaves(leaveappldetails.EmpCode, leaveappldetails.LeaveDays, context);

                            leaveappldetails.Status = STATUS.NEW.ToString();
                            leaveappldetails.AddDate =  DateTime.Now;
                            context.LeaveApplDetails.Add(leaveappldetails);
                            if (context.SaveChanges() > 0)
                            {
                                transaction.Commit();
                                status = RETIRNSTATUS.PASS;
                                return leaveappldetails;
                            }
                            transaction.Rollback();
                            errormsg = "error occured, failed to apply leaves";
                            status = RETIRNSTATUS.FAIL;
                            return null;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //  public bool validateleaves(string leavecode,string employeeid, datetime? fromdate, datetime? todate, out string errormsg)
        public bool validateleaves(string leavecode, string employeeid, decimal nofdays, out string errormsg)
        {
            try
            {
                errormsg = string.Empty;

                using (MobileContext context = new MobileContext())
                {
                    LeaveBalanceMaster leavebalancemaster = context.LeaveBalanceMaster
                                                           .Where(l => l.LeaveCode == leavecode && l.EmpCode == employeeid).FirstOrDefault();


                    if(leavebalancemaster == null)
                    {
                        errormsg = "No leave balace configured from employee :"+employeeid;
                        return false;
                    }


                    //   if (calculatenoofdays(fromdate, todate) > leavebalancemaster.balance)
                    if (nofdays > Convert.ToDecimal(leavebalancemaster.Balance))
                    {
                        errormsg = "desont have sucfficient days";
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public Employees getemployee(string employeecode)
        {
            try
            {
                using (MobileContext context = new MobileContext())
                {
                    return context.Employees.Where(emp => emp.Code == employeecode).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void updateusedleaves(string empcode, double? noofleaves, MobileContext context,bool isLeaveCanceled=false)
        {
            try
            {
                var leavebalancemaster = context.LeaveBalanceMaster.Where(x => x.UserId == empcode).FirstOrDefault();

                if (isLeaveCanceled)
                {
                    leavebalancemaster.Used -= noofleaves;
                    leavebalancemaster.Balance += noofleaves;
                }
                else
                {
                    leavebalancemaster.Used += noofleaves;
                    leavebalancemaster.Balance -= noofleaves;
                }
                
                context.LeaveBalanceMaster.Update(leavebalancemaster);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<string> GetLeavStatusList()
        {
            try
            {
                return new List<string>() { STATUS.APPROVED.ToString(), STATUS.INPROGRESS.ToString(), STATUS.CANCEL.ToString() };
            }
            catch { throw; }
        }
    }
}
