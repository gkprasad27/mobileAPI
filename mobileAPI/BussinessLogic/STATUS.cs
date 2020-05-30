using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mobileAPI.BussinessLogic
{
    public enum STATUS
    {
       APPROVED
      ,CANCEL
      ,NEW
      ,COMPLETE
      ,REJECT
      ,INPROGRESS
    }

    public enum RETIRNSTATUS
    {
        PASS
       ,FAIL
    }

    public enum VISITTYPE
    {
        SALES,
        SERVICEENGINEER
    }
}
