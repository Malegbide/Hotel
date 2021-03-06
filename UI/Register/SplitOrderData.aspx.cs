﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.FrontDesk;
using System.Text.RegularExpressions;

public partial class Register_SplitOrderData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string action = Request["action"];
        string fh=Request["fh"];
        if (!string.IsNullOrEmpty(fh))
        {
            OrdersHelper helper = new OrdersHelper();
            string str = Regex.Replace(Utils.ToRecordJson(helper.ReadAllAttachByFH(fh)), @"\\/Date\((\d+)\)\\/", match =>
            {
                DateTime dt = new DateTime(1970, 1, 1);
                dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
                dt = dt.ToLocalTime();
                return dt.ToString("yyyy-MM-dd HH:mm:ss");
            });
            Response.Write(str);
        }
    }
}