﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using WebGrease.Css.Extensions;

namespace NFI.Helper
{
    public static class HtmlExtensions
    {
        public static string GetHostServerUrl(this System.Web.Mvc.HtmlHelper htmlHelper)
        {
            return Properties.Settings.Default.HostServerUrl;
        }

        public static string GetWaitMessageForApplication(this System.Web.Mvc.HtmlHelper htmlHelper)
        {
            return "Din søknad er opplasting. Vennligst vent...";
        }

        public static string GetWaitMessageForApplicationEnglish(this System.Web.Mvc.HtmlHelper htmlHelper)
        {
            return "Your application is uploading. Please wait...";
        }

        /// <summary>
        /// Returns a checkbox for each of the provided <paramref name="items"/>.
        /// </summary>
        public static MvcHtmlString CheckBoxListFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> items, object htmlAttributes = null)
        {
            var listName = ExpressionHelper.GetExpressionText(expression);
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var baseAttr = htmlHelper.GetUnobtrusiveValidationAttributes(listName, metaData);
            var htmlAtributelist = new RouteValueDictionary(htmlAttributes);
            if (metaData.IsRequired)
            {
                baseAttr.Add("data-val-requiredgroup", baseAttr["data-val-required"]);
                baseAttr.Remove("data-val-required");
            }
            items = GetCheckboxListWithDefaultValues(metaData.Model, items);
            return htmlHelper.CheckBoxList(listName, items, new RouteValueDictionary(baseAttr), htmlAtributelist);
        }
        /// <summary>
        /// Returns a checkbox for each of the provided <paramref name="items"/>.
        /// </summary>
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string listName, IEnumerable<SelectListItem> items, RouteValueDictionary checkBoxAttribute, RouteValueDictionary htmlAttributes = null)
        {
            var container = new TagBuilder("div");
            foreach (var item in items)
            {
                var label = new TagBuilder("label");
                label.MergeAttribute("class", "checkbox"); // default class
                label.MergeAttributes(htmlAttributes, true);
                var cb = new TagBuilder("input");
                cb.MergeAttribute("type", "checkbox");
                cb.MergeAttribute("name", listName);
                cb.MergeAttribute("value", item.Value ?? item.Text);
                cb.MergeAttributes(checkBoxAttribute, true);
                if (item.Selected)
                    cb.MergeAttribute("checked", "checked");
                label.InnerHtml = cb.ToString(TagRenderMode.SelfClosing) + item.Text;
                container.InnerHtml += label.ToString();
            }

            return new MvcHtmlString(container.ToString());
        }
        private static IEnumerable<SelectListItem> GetCheckboxListWithDefaultValues(object defaultValues, IEnumerable<SelectListItem> selectList)
        {
            var defaultValuesList = defaultValues as IEnumerable;
            if (defaultValuesList == null)
                return selectList;
            var values = from object value in defaultValuesList
                         select Convert.ToString(value, CultureInfo.CurrentCulture);
            var selectedValues = new HashSet<string>(values, StringComparer.OrdinalIgnoreCase);
            var newSelectList = new List<SelectListItem>();
            selectList.ForEach(item =>
            {
                item.Selected = (item.Value != null) ? selectedValues.Contains(item.Value) : selectedValues.Contains(item.Text);
                newSelectList.Add(item);
            });

            return newSelectList;
        }

    }
}