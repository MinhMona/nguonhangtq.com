﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
namespace NHST.Controllers
{
    public class PageTypeController
    {
        #region CRUD
        public static string Insert(string PageTypeName, string PageTypeDescription, int IndexPos, int NodeID, string NodeAliasPath,
            string ogurl, string ogtitle, string ogdescription, string ogimage, string metatitle,
            string metadescription, string metakeyword, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_PageType p = new tbl_PageType();
                p.PageTypeName = PageTypeName;
                p.PageTypeDescription = PageTypeDescription;
                p.IndexPos = IndexPos;
                p.NodeID = NodeID;
                p.ogurl = ogurl;
                p.ogtitle = ogtitle;
                p.ogdescription = ogdescription;
                p.ogimage = ogimage;
                p.metatitle = metatitle;
                p.metadescription = metadescription;
                p.metakeyword = metakeyword;
                p.NodeAliasPath = NodeAliasPath;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_PageType.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, string PageTypeName, string PageTypeDescription, int IndexPos, int NodeID, string NodeAliasPath,
            string ogurl, string ogtitle, string ogdescription, string ogimage, string metatitle,
            string metadescription, string metakeyword, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_PageType.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.PageTypeName = PageTypeName;
                    p.PageTypeDescription = PageTypeDescription;
                    p.IndexPos = IndexPos;
                    p.NodeID = NodeID;
                    p.NodeAliasPath = NodeAliasPath;
                    p.ogurl = ogurl;
                    p.ogtitle = ogtitle;
                    p.ogdescription = ogdescription;
                    p.ogimage = ogimage;
                    p.metatitle = metatitle;
                    p.metadescription = metadescription;
                    p.metakeyword = metakeyword;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        #endregion
        #region Select
        public static List<tbl_PageType> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_PageType> pages = new List<tbl_PageType>();
                pages = dbe.tbl_PageType.OrderBy(p => p.IndexPos).ToList();
                if (pages.Count > 0)
                {
                    return pages;
                }
                else return null;
            }
        }

        public static tbl_PageType GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_PageType page = dbe.tbl_PageType.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        public static tbl_PageType GetByNodeAliasPath(string NodeAliasPath)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_PageType page = dbe.tbl_PageType.Where(p => p.NodeAliasPath == NodeAliasPath).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        #endregion
    }
}