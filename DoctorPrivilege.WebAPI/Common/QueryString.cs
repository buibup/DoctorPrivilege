using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorPrivilege.WebAPI.Common
{
    public class QueryString
    {
        public static string GetAllDoctor()
        {
            string result = @"

            SELECT top 10 CTPCP_Code DoctorID
            ,CTPCP_Spec_DR->CTSPC_Desc Specialty
            ,CTPCP_SubSpec_DR->CTSPC_Desc SubSpecialty
            ,CTPCP_CarPrvTp_DR->CTCPT_Desc Category
            FROM ct_careprov
            where CTPCP_Code like '119%'
            and CTPCP_DateActiveTo is null
            ";

            return result;
        }

        public static string GetProcedure(string doctorId)
        {
            string result = @"

                SELECT PRC_NME ProcedureName, Comments Comment, Approve_Privilege Grade
                  FROM [PTSYS].[dbo].[V_DRPRITRA]
                  Where DR_ID = '{doctorId}'

            ";

            result = result.Replace("{doctorId}", doctorId);

            return result;
        }

        public static string GetApprovedDate(string doctorId)
        {
            string result = @"

            Select Max(PRI_DTE) ApprovedDate
            From DRPRIDEP
            Where DR_ID = '{doctorId}'

            ";

            result = result.Replace("{doctorId}", doctorId);

            return result;
        }

        public static string GetProcedureModel(string buid)
        {
            string result = @"
            

            select cast( A.A_DR_ID as varchar) [Doctor ID], cast( A.A_SPC as varchar) [Specialty], cast( B.A_SUB_SPC as varchar) [Sub Specialty] , '' [Category]
            , cast( D.PRC_NME as varchar) [Procedure], cast(C.Comments as varchar) [Comment], cast( C.Approve_Privilege as varchar) [Grade]
            , case when isnull(C.Approve_Privilege,'') <> '' then 'A' else '' end [Status]
            ,(Select Max(PRI_DTE) From [PTSYS].[dbo].DRPRIDEP E where C.DR_ID = E.DR_ID) [Approved Date]
            from [dbo].[PS_A_DRNME] A
            left join [dbo].[PS_A_DRSUB] B
            on A.A_DR_ID = B.A_DR_ID
            left join [PTSYS].[dbo].DRPRITRA C
            on A.A_DR_ID = C.DR_ID COLLATE DATABASE_DEFAULT
            Left join [PTSYS].[dbo].DRDIVPRC D
            on C.PRC_ROWID = D.PRC_ROWID
            Where substring(A.A_LOC_CDE,1,2) = '{buid}'
            Group by cast( A.A_DR_ID as varchar), cast( A.A_SPC as varchar), cast( B.A_SUB_SPC as varchar)
            , cast( D.PRC_NME as varchar), cast( C.Comments as varchar), cast( C.Approve_Privilege as varchar)
            , C.Approve_Privilege,  C.DR_ID 

            
            ";

            result = result.Replace("{buid}", buid);

            return result;
        }
    }
}