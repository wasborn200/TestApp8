﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestApp8.Util
{
    public class PrefuctureDropDown
    {
        public static List<SelectListItem> getSelectListItem()
        {
            List<SelectListItem> selectoptions = new List<SelectListItem>();
            var group1 = new SelectListGroup() { Name = "北海道・東北" };
            var group2 = new SelectListGroup() { Name = "関東" };
            var group3 = new SelectListGroup() { Name = "甲信越・北陸" };
            var group4 = new SelectListGroup() { Name = "東海" };
            var group5 = new SelectListGroup() { Name = "関西" };
            var group6 = new SelectListGroup() { Name = "中国・四国" };
            var group7 = new SelectListGroup() { Name = "九州・沖縄" };

            selectoptions.Add(new SelectListItem() { Value = "1", Text = "北海道", Group = group1 });
            selectoptions.Add(new SelectListItem() { Value = "2", Text = "青森県", Group = group1 });
            selectoptions.Add(new SelectListItem() { Value = "3", Text = "岩手県", Group = group1 });
            selectoptions.Add(new SelectListItem() { Value = "4", Text = "宮城県", Group = group1 });
            selectoptions.Add(new SelectListItem() { Value = "5", Text = "秋田県", Group = group1 });
            selectoptions.Add(new SelectListItem() { Value = "6", Text = "山形県", Group = group1 });
            selectoptions.Add(new SelectListItem() { Value = "7", Text = "福島県", Group = group1 });
            selectoptions.Add(new SelectListItem() { Value = "8", Text = "茨城県", Group = group2 });
            selectoptions.Add(new SelectListItem() { Value = "9", Text = "栃木県", Group = group2 });
            selectoptions.Add(new SelectListItem() { Value = "10", Text = "群馬県", Group = group2 });
            selectoptions.Add(new SelectListItem() { Value = "11", Text = "埼玉県", Group = group2 });
            selectoptions.Add(new SelectListItem() { Value = "12", Text = "千葉県", Group = group2 });
            selectoptions.Add(new SelectListItem() { Value = "13", Text = "東京都", Group = group2 });
            selectoptions.Add(new SelectListItem() { Value = "14", Text = "神奈川県", Group = group2 });
            selectoptions.Add(new SelectListItem() { Value = "15", Text = "新潟県", Group = group3 });
            selectoptions.Add(new SelectListItem() { Value = "16", Text = "富山県", Group = group3 });
            selectoptions.Add(new SelectListItem() { Value = "17", Text = "石川県", Group = group3 });
            selectoptions.Add(new SelectListItem() { Value = "18", Text = "福井県", Group = group3 });
            selectoptions.Add(new SelectListItem() { Value = "19", Text = "山梨県", Group = group3 });
            selectoptions.Add(new SelectListItem() { Value = "20", Text = "長野県", Group = group3 });
            selectoptions.Add(new SelectListItem() { Value = "21", Text = "岐阜県", Group = group4 });
            selectoptions.Add(new SelectListItem() { Value = "22", Text = "静岡県", Group = group4 });
            selectoptions.Add(new SelectListItem() { Value = "23", Text = "愛知県", Group = group4 });
            selectoptions.Add(new SelectListItem() { Value = "24", Text = "三重県", Group = group4 });
            selectoptions.Add(new SelectListItem() { Value = "25", Text = "滋賀県", Group = group5 });
            selectoptions.Add(new SelectListItem() { Value = "26", Text = "京都府", Group = group5 });
            selectoptions.Add(new SelectListItem() { Value = "27", Text = "大阪府", Group = group5 });
            selectoptions.Add(new SelectListItem() { Value = "28", Text = "兵庫県", Group = group5 });
            selectoptions.Add(new SelectListItem() { Value = "29", Text = "奈良県", Group = group5 });
            selectoptions.Add(new SelectListItem() { Value = "30", Text = "和歌山県", Group = group5 });
            selectoptions.Add(new SelectListItem() { Value = "31", Text = "鳥取県", Group = group6 });
            selectoptions.Add(new SelectListItem() { Value = "32", Text = "島根県", Group = group6 });
            selectoptions.Add(new SelectListItem() { Value = "33", Text = "岡山県", Group = group6 });
            selectoptions.Add(new SelectListItem() { Value = "34", Text = "広島県", Group = group6 });
            selectoptions.Add(new SelectListItem() { Value = "35", Text = "山口県", Group = group6 });
            selectoptions.Add(new SelectListItem() { Value = "36", Text = "徳島県", Group = group6 });
            selectoptions.Add(new SelectListItem() { Value = "37", Text = "香川県", Group = group6 });
            selectoptions.Add(new SelectListItem() { Value = "38", Text = "愛媛県", Group = group6 });
            selectoptions.Add(new SelectListItem() { Value = "39", Text = "高知県", Group = group6 });
            selectoptions.Add(new SelectListItem() { Value = "40", Text = "福岡県", Group = group7 });
            selectoptions.Add(new SelectListItem() { Value = "41", Text = "佐賀県", Group = group7 });
            selectoptions.Add(new SelectListItem() { Value = "42", Text = "長崎県", Group = group7 });
            selectoptions.Add(new SelectListItem() { Value = "43", Text = "熊本県", Group = group7 });
            selectoptions.Add(new SelectListItem() { Value = "44", Text = "大分県", Group = group7 });
            selectoptions.Add(new SelectListItem() { Value = "45", Text = "宮崎県", Group = group7 });
            selectoptions.Add(new SelectListItem() { Value = "46", Text = "鹿児島県", Group = group7 });
            selectoptions.Add(new SelectListItem() { Value = "47", Text = "沖縄県", Group = group7 });




            //selectoptions.Add(new SelectListItem() { Value = "1", Text = "東京", Group = group1 });
            //selectoptions.Add(new SelectListItem() { Value = "2", Text = "大阪", Group = group1 });
            //selectoptions.Add(new SelectListItem() { Value = "3", Text = "愛知", Group = group2 });
            //selectoptions.Add(new SelectListItem() { Value = "4", Text = "福岡", Group = group2 });



            return selectoptions;
        }
    }
}