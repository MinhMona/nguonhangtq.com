﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;

namespace NHST.Controllers
{
    public class CustomerBenefitsController
    {
        #region CRUD
        public static tbl_CustomerBenefits Insert(string Icon, string CustomerBenefitName, string CustomerBenefitDescription, string CustomerBenefitLink, bool IsHidden, int Position, string CreatedBy)
        {
            using (var db = new NHSTEntities())
            {
                tbl_CustomerBenefits ctb = new tbl_CustomerBenefits();
                ctb.Icon = Icon;
                ctb.CustomerBenefitName = CustomerBenefitName;
                ctb.CustomerBenefitDescription = CustomerBenefitDescription;
                ctb.CustomerBenefitLink = CustomerBenefitLink;
                ctb.IsHidden = IsHidden;
                ctb.Position = Position;
                ctb.CreatedBy = CreatedBy;
                ctb.CreatedDate = DateTime.Now;
                db.tbl_CustomerBenefits.Add(ctb);
                db.SaveChanges();
                return ctb;
            }
        }

        public static tbl_CustomerBenefits Update(int ID, string Icon, string CustomerBenefitName, string CustomerBenefitDescription, string CustomerBenefitLink, bool IsHidden, int Position, string CreatedBy)
        {
            using (var db = new NHSTEntities())
            {
                var ctb = db.tbl_CustomerBenefits.Where(x => x.ID == ID).FirstOrDefault();
                if (ctb != null)
                {
                    ctb.Icon = Icon;
                    ctb.CustomerBenefitName = CustomerBenefitName;
                    ctb.CustomerBenefitDescription = CustomerBenefitDescription;
                    ctb.CustomerBenefitLink = CustomerBenefitLink;
                    ctb.IsHidden = IsHidden;
                    ctb.Position = Position;
                    ctb.ModifiedBy = CreatedBy;
                    ctb.ModifiedDate = DateTime.Now;
                    db.SaveChanges();
                    return ctb;
                }
                return null;
            }
        }
        #endregion

        #region Select
        public static List<tbl_CustomerBenefits> GetAll(string CustomerBenefitName)
        {
            using (var db = new NHSTEntities())
            {
                var ctb = db.tbl_CustomerBenefits.Where(x => x.IsHidden != true && x.CustomerBenefitName.Contains(CustomerBenefitName)).OrderBy(x => x.Position).ToList();
                if (ctb.Count > 0)
                    return ctb;
                return null;
            }
        }

        public static List<tbl_CustomerBenefits> GetAllAD(string CustomerBenefitName)
        {
            using (var db = new NHSTEntities())
            {
                var ctb = db.tbl_CustomerBenefits.Where(x => x.CustomerBenefitName.Contains(CustomerBenefitName)).OrderBy(x => x.Position).ToList();
                if (ctb.Count > 0)
                    return ctb;
                return null;
            }
        }

        public static tbl_CustomerBenefits GetByID(int ID)
        {
            using (var db = new NHSTEntities())
            {
                var ctb = db.tbl_CustomerBenefits.Where(x => x.ID == ID).FirstOrDefault();
                if (ctb != null)
                    return ctb;
                return null;
            }
        }
        #endregion
    }
}