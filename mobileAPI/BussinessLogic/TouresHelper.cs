using mobileAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace mobileAPI.BussinessLogic
{
    public class TouresHelper
    {
        public List<VisitResion> GetVisitResions(string visitType)
        {
            try
            {
                using (MobileContext context = new MobileContext())
                {
                    return context.VisitResion
                                 .AsEnumerable()
                                .Where(x => Regex.Replace(x.VisitType, @"\s+", "").ToLower() == Regex.Replace(visitType, @"\s+", "").ToLower())
                                .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetVisitTypes()
        {
            try
            {
                using (MobileContext context = new MobileContext())
                {
                    return context.VisitResion.Select(x => x.VisitType).Distinct().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Product> GetProducts()
        {
            try
            {
                using (MobileContext context = new MobileContext())
                {
                    return context.Product.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetStatuses()
        {
            return new List<string>() { STATUS.APPROVED.ToString(), STATUS.REJECT.ToString() };
        }

        public TourAdvance InsertTour(TourAdvance tourAdvance, out string errorMessage)
        {
            try
            {
               // var date = DateTime.ParseExact(tourAdvance.FromJourneyDate, "dd/MM/yyyy hh:mm:ss tt", null).ToString("dd/MM/yyyy hh:mm:ss tt");
                errorMessage = string.Empty;

                using (MobileContext context = new MobileContext())
                {


                    //var _count = context.TourAdvance.AsEnumerable()
                    //                    .Where(x =>COnver(tourAdvance.FromJourneyDate, "dd/MM/yyyy hh:mm:ss tt", null) >= DateTime.ParseExact(tourAdvance.FromJourneyDate, "dd/MM/yyyy hh:mm:ss tt", null)
                    //                                               && tourAdvance.EmpCode == tourAdvance.EmpCode).Count();

                    //if (_count > 0)
                    //{
                    //    errorMessage = "Same journey alrady applied";
                    //    return null;
                    //}


                    tourAdvance.Status = STATUS.NEW.ToString();
                    tourAdvance.ApplyDate = (DateTime.Now);
                    tourAdvance.FromJourneyDate = tourAdvance.FromJourneyDate;
                    tourAdvance.ToJourneyDate = tourAdvance.ToJourneyDate;
                    context.TourAdvance.Add(tourAdvance);
                    if (context.SaveChanges() > 0)
                    {
                        return tourAdvance;
                    }

                    return null;
                }
            }
            catch(Exception ex)
            {
               
                throw ex;
            }
        }
        public List<TourAdvance> GetTourForApporval(string empCode)
        {
            try
            {
                List<TourAdvance> _TourAdvance = new List<TourAdvance>();

                using (MobileContext context = new MobileContext())
                {
                    var _TourAdvanceList = (from toure in context.TourAdvance
                                            join emp in context.Employees
                                            on toure.EmpCode equals emp.Code
                                            where emp.ApprovedBy == empCode
                                               && toure.Status != STATUS.APPROVED.ToString()
                                               && toure.Status != STATUS.CANCEL.ToString()
                                            select toure).ToList();

                    return _TourAdvanceList;
                }
            }
            catch
            {
                throw;
            }
        }
        public List<TourAdvance> TourApprovalProcess(string empcode,List<TourAdvance> toursList)
        {
            try
            {
                List<TourAdvance> processToures = new List<TourAdvance>();
                using (MobileContext context=new MobileContext())
                {
                    foreach (var tours in toursList) 
                    {
                        tours.ApprovedBy = empcode;
                        context.TourAdvance.Update(tours);
                        if (context.SaveChanges() > 0)
                            processToures.Add(tours);
                    }
                }

                return processToures;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<TourAdvance> GetArrovedToures(string empCode)
        {
            try
            {
                using (MobileContext context = new MobileContext())
                {
                       
                   // return  context.TourAdvance.Where(t=> t.EmpCode == empCode && t.Status == STATUS.APPROVED.ToString()).ToList();
                    return  context.TourAdvance.Where(t=> t.EmpCode == empCode).ToList();
                      
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<TourAdvance> GetTours(string empCode)
        {
            try
            {
                using(MobileContext context=new MobileContext())
                {
                    return context.TourAdvance.Where(x => x.EmpCode == empCode).ToList();
                }

            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<Visit> GetVisitsOfEmployee(string employeeCode)
        {
            try
            {
                using (MobileContext context = new MobileContext())
                {
                    return context.Visit
                           .Where(v => v.VisitedEmployee == employeeCode).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Visit  InsertVisit(string employeeCode,Visit visit,string visitType)
        {
            try
            {
                using (MobileContext context = new MobileContext())
                {
                    visit.VisitType = visitType;
                    visit.VisitedEmployee = employeeCode;
                    visit.AddDate = DateTime.Now;
                    context.Add(visit);
                    if (context.SaveChanges() > 0)
                        return visit;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<Companies> GetClientsList()
        {
            try
            {
                using (MobileContext context=new MobileContext())
                {
                    return context.Companies.ToList();
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public Companies InsertCompanies(Companies companies,out string errorMessage)
        {
            try
            {
                errorMessage = string.Empty;

                using (MobileContext context=new MobileContext())
                {
                    var _code = context.Companies.Max(x => x.CompanyCode)?? "0";

                    companies.CompanyCode = (Convert.ToInt32(_code) + 1).ToString();
                    companies.Active = "Y";
                    companies.AddDate = DateTime.Now;

                    context.Add(companies);
                    if (context.SaveChanges() > 0)
                        return companies;

                    errorMessage = "failed to create client.";
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #region Bills
        public TblTourBills AddToureBills(TblTourBills tblTourBills) 
        {
            try
            {
                tblTourBills.AppliedDate = DateTime.Now;
                using (MobileContext context=new MobileContext())
                {
                    context.TblTourBills.Add(tblTourBills);
                    if(context.SaveChanges()> 0)
                    return tblTourBills;

                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<TblTourBills> GetToureBills(string employeeId,string tourID)
        {
            try
            {
                employeeId = string.IsNullOrEmpty(employeeId) ? null :employeeId;
                tourID = string.IsNullOrEmpty(tourID) ? null : tourID;

                using (MobileContext context = new MobileContext())
                {
                    return context.TblTourBills
                                  .Where(x=> x.Tourid ==  (tourID ?? x.Tourid)
                                          && x.EmpId == (employeeId ?? x.EmpId)
                                          && x.AppliedDate.Value.Year == DateTime.Today.Year
                                  ).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
