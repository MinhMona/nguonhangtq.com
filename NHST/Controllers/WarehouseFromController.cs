﻿using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHST.Controllers
{
    public class WarehouseFromController
    {
        #region CRUD
        public static string Insert(string WareHouseName, double AdditionFee, string Address, string Email, string Phone,
            string Latitude, string Longitude, bool IsHidden, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_WarehouseFrom c = new tbl_WarehouseFrom();
                c.WareHouseName = WareHouseName;
                c.AdditionFee = AdditionFee;
                c.Address = Address;
                c.Email = Email;
                c.Phone = Phone;
                c.Latitude = Latitude;
                c.Longitude = Longitude;
                c.IsHidden = IsHidden;
                c.CreatedDate = CreatedDate;
                c.CreatedBy = CreatedBy;
                dbe.tbl_WarehouseFrom.Add(c);
                dbe.SaveChanges();
                string kq = c.ID.ToString();
                return kq;
            }
        }
        public static string Update(int ID, string WareHouseName, double AdditionFee, string Address, string Email, string Phone,
            string Latitude, string Longitude, bool IsHidden, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_WarehouseFrom.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    c.WareHouseName = WareHouseName;
                    c.AdditionFee = AdditionFee;
                    c.Address = Address;
                    c.Email = Email;
                    c.Phone = Phone;
                    c.Latitude = Latitude;
                    c.Longitude = Longitude;
                    c.IsHidden = IsHidden;
                    c.ModifiedDate = ModifiedDate;
                    c.ModifiedBy = ModifiedBy;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select        
        public static List<tbl_WarehouseFrom> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_WarehouseFrom> cs = new List<tbl_WarehouseFrom>();
                //cs = dbe.tbl_WarehouseFrom.Where(c => c.WareHouseName.Contains(s)).OrderByDescending(c => c.ID).ToList();
                cs = dbe.tbl_WarehouseFrom.Where(c => c.WareHouseName.Contains(s)).ToList();
                return cs;
            }
        }
        public static List<tbl_WarehouseFrom> GetAllWithIsHidden(bool IsHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_WarehouseFrom> cs = new List<tbl_WarehouseFrom>();
                cs = dbe.tbl_WarehouseFrom.Where(c => c.IsHidden == IsHidden).OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static tbl_WarehouseFrom GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_WarehouseFrom.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    return c;
                }
                else
                    return null;
            }
        }
        #endregion
    }
}