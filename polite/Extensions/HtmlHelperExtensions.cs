using polite.Models;
using polite.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

public static class HtmlHelperExtensions
{
    public static HtmlString EncodeAndProcess(this HtmlHelper htmlHelper, 
        string contents, Post post)
    {
        string encoded = htmlHelper.Encode(contents);
        var crossLinkRegex = new Regex("&gt;&gt;&gt;/(\\w+)/(\\d+)");
        foreach (Match m in crossLinkRegex.Matches(encoded))
        {
            string originalText = m.Groups[0].Value;
            string refBoard = m.Groups[1].Value;
            string referredTo = m.Groups[2].Value;
            int postID = int.Parse(referredTo);
            string link = getCrossLinkFor(originalText, refBoard, postID, post);
            encoded = encoded.Replace(originalText, link);
        }
        var linkRegex = new Regex("&gt;&gt;(\\d+)");
        foreach (Match m in linkRegex.Matches(encoded))
        {
            string originalText = m.Groups[0].Value;
            string referredTo = m.Groups[1].Value;
            int postID = int.Parse(referredTo);
            string link = getLinkFor(originalText, postID, post);
            encoded = encoded.Replace(originalText, link);
        }
        var r = new Regex("^&gt;(.*)$", RegexOptions.Multiline);
        foreach (Match m in r.Matches(encoded))
        {
            encoded = r.Replace(encoded, "<span style=\"color:green\">&gt;$1</span>");
        }
        var newlineRegex = new Regex("\r?\n");
        if (newlineRegex.IsMatch(encoded))
        {
            encoded = newlineRegex.Replace(encoded, "<br>");
        }
        return new HtmlString(encoded);
    }

    private static string getLinkFor(string originalText, int postID, Post post)
    {
        PostService service = new PostService();
        string retVal = service.getLinkFor(originalText, postID, post);
        return retVal;
    }

    private static string getCrossLinkFor(string originalText, string refBoard, int postID, Post post)
    {
        PostService service = new PostService();
        string retVal = service.getCrossLinkFor(originalText, refBoard, postID, post);
        return retVal;
    }
}