using System;
using System.Collections.Generic;
using System.Web;

namespace Link2Web.Helpers
{
    public class MySession
    {
        // private constructor
        private MySession()
        {
            FillOrder = new List<string>();
        }

        // Gets the current session.
        public static MySession Current
        {
            get
            {
                var session = (MySession)HttpContext.Current.Session["__MySession__"];
                if (session == null)
                {
                    session = new MySession();
                    HttpContext.Current.Session["__MySession__"] = session;
                }
                return session;
            }
        }

        public static void Clear()
        {
            MySession session =
                (MySession)HttpContext.Current.Session["__MySession__"];
            if (session != null)
            {
                HttpContext.Current.Session.Remove("__MySession__");
            }
        }

        // **** add your session properties here, e.g like this:
        public List<string> FillOrder { get; set; }
        public string Property1 { get; set; }
        public DateTime MyDate { get; set; }
        public int LoginId { get; set; }
    }
}