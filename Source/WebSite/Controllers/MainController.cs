﻿//******************************************************************************************************
//  HomeController.cs - Gbtc
//
//  Copyright © 2016, Grid Protection Alliance.  All Rights Reserved.
//
//  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
//  the NOTICE file distributed with this work for additional information regarding copyright ownership.
//  The GPA licenses this file to you under the MIT License (MIT), the "License"; you may not use this
//  file except in compliance with the License. You may obtain a copy of the License at:
//
//      http://opensource.org/licenses/MIT
//
//  Unless agreed to in writing, the subject software distributed under the License is distributed on an
//  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
//  License for the specific language governing permissions and limitations.
//
//  Code Modification History:
//  ----------------------------------------------------------------------------------------------------
//  02/17/2016 - J. Ritchie Carroll
//       Generated original version of source code.
//
//******************************************************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using GSF.Data;
using GSF.Data.Model;
using GSF.Web.Model;
using GSF.Web.Security;
using SOETools.Model;

namespace SOETools.Controllers
{
    /// <summary>
    /// Represents a MVC controller for the site's main pages.
    /// </summary>
    [AuthorizeControllerRole]
    public class MainController : Controller
    {
        #region [ Members ]

        // Fields
        private readonly DataContext m_dataContext;
        private readonly DataContext m_dbContext;
        private readonly AppModel m_appModel;
        private readonly AppModel m_dbModel;
        private bool m_disposed;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Creates a new <see cref="MainController"/>.
        /// </summary>
        public MainController()
        {
            // Establish data context for the view
            m_dataContext = new DataContext(exceptionHandler: MvcApplication.LogException);
            m_dbContext = new DataContext("thirdDb", exceptionHandler: MvcApplication.LogException);
            ViewData.Add("DataContext", m_dataContext);
            ViewData.Add("DbContext", m_dbContext);

            // Set default model for pages used by layout
            m_appModel = new AppModel(m_dataContext);
            m_dbModel = new AppModel(m_dbContext);
            ViewData.Add("DbModel", m_dbModel);
            ViewData.Model = m_appModel;
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="MainController"/> object and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                try
                {
                    if (disposing)
                    {
                        m_dataContext?.Dispose();
                        m_appModel?.Dispose();
                    }
                }
                finally
                {
                    m_disposed = true;          // Prevent duplicate dispose.
                    base.Dispose(disposing);    // Call base class Dispose().
                }
            }
        }

        public ActionResult Home()
        {
            m_appModel.ConfigureView(Url.RequestContext, "Home", ViewBag);
            int groupID = m_dataContext.Connection.ExecuteScalar<int?>("Select ID From ValueListGroup Where Name = 'timeWindows'") ?? 0;
            //DataTable incidentCountTable = m_dbContext.Connection.RetrieveData("SELECT IncidentType, COUNT(*) AS IncidentCount FROM IncidentEventCycleDataView GROUP BY IncidentType");

            ViewBag.timeWindows = m_dataContext.Table<ValueList>().QueryRecords(restriction: new RecordRestriction("GroupID = {0}", groupID)).ToArray();
            //ViewBag.SOESAD = incidentCountTable.AsEnumerable().Select(row => row.ConvertField<int>("IncidentCount")).DefaultIfEmpty(0).Sum();
            //ViewBag.FaultsAT = incidentCountTable.Select("IncidentType IN ('LG', 'LLG', 'LLLG', 'LL', 'LLL')").Select(row => row.ConvertField<int>("IncidentCount")).DefaultIfEmpty(0).Sum();
            //ViewBag.FaultsLGAT = incidentCountTable.Select("IncidentType IN ('LG', 'LLG', 'LLLG')").Select(row => row.ConvertField<int>("IncidentCount")).DefaultIfEmpty(0).Sum();
            //ViewBag.FaultsLLAT = incidentCountTable.Select("IncidentType = 'LL'").Select(row => row.ConvertField<int>("IncidentCount")).DefaultIfEmpty(0).Sum();
            //ViewBag.FaultsLLLAT = incidentCountTable.Select("IncidentType = 'LLL'").Select(row => row.ConvertField<int>("IncidentCount")).DefaultIfEmpty(0).Sum();
            //ViewBag.VoltsAT = incidentCountTable.Select("IncidentType LIKE '%SAG' OR IncidentType LIKE '%SWELL'").Select(row => row.ConvertField<int>("IncidentCount")).DefaultIfEmpty(0).Sum();
            //ViewBag.VoltSAGAT = incidentCountTable.Select("IncidentType LIKE '%SAG'").Select(row => row.ConvertField<int>("IncidentCount")).DefaultIfEmpty(0).Sum();
            //ViewBag.VoltSWELLAT = incidentCountTable.Select("IncidentType LIKE '%SWELL'").Select(row => row.ConvertField<int>("IncidentCount")).DefaultIfEmpty(0).Sum();
            //ViewBag.OtherAT = incidentCountTable.Select("IncidentType = 'OTHER'").Select(row => row.ConvertField<int>("IncidentCount")).DefaultIfEmpty(0).Sum();

            //List<int> counts = new List<int>();
            //List<int> faults = new List<int>();
            //List<int> faultsLG = new List<int>();
            //List<int> faultsLL = new List<int>();
            //List<int> faultsLLL = new List<int>();
            //List<int> volts = new List<int>();
            //List<int> others = new List<int>();
            //List<int> voltsags = new List<int>();
            //List<int> voltswells = new List<int>();

            //foreach (ValueList vl in ViewBag.timeWindows)
            //{
            //    incidentCountTable = m_dbContext.Connection.RetrieveData("SELECT IncidentType, COUNT(*) AS IncidentCount FROM IncidentEventCycleDataView WHERE DATEDIFF(day, StartTime, GETDATE()) <= {0} GROUP BY IncidentType", vl.Value);
            //    counts.Add(incidentCountTable.AsEnumerable().Select(row => row.ConvertField<int>("IncidentCount")).DefaultIfEmpty(0).Sum());
            //    faults.Add(incidentCountTable.Select("IncidentType IN ('LG', 'LLG', 'LLLG', 'LL', 'LLL')").Select(row => row.ConvertField<int>("IncidentCount")).DefaultIfEmpty(0).Sum());
            //    faultsLG.Add(incidentCountTable.Select("IncidentType IN ('LG', 'LLG', 'LLLG')").Select(row => row.ConvertField<int>("IncidentCount")).DefaultIfEmpty(0).Sum());
            //    faultsLL.Add(incidentCountTable.Select("IncidentType = 'LL'").Select(row => row.ConvertField<int>("IncidentCount")).DefaultIfEmpty(0).Sum());
            //    faultsLLL.Add(incidentCountTable.Select("IncidentType = 'LLL'").Select(row => row.ConvertField<int>("IncidentCount")).DefaultIfEmpty(0).Sum());
            //    volts.Add(incidentCountTable.Select("IncidentType IN ('SAG', 'SWELL')").Select(row => row.ConvertField<int>("IncidentCount")).DefaultIfEmpty(0).Sum());
            //    voltsags.Add(incidentCountTable.Select("IncidentType = 'SAG'").Select(row => row.ConvertField<int>("IncidentCount")).DefaultIfEmpty(0).Sum());
            //    voltswells.Add(incidentCountTable.Select("IncidentType = 'SWELL'").Select(row => row.ConvertField<int>("IncidentCount")).DefaultIfEmpty(0).Sum());
            //    others.Add(incidentCountTable.Select("IncidentType = 'OTHER'").Select(row => row.ConvertField<int>("IncidentCount")).DefaultIfEmpty(0).Sum());
            //}


            //ViewBag.counts = counts;
            //ViewBag.faults = faults;
            //ViewBag.faultsLG = faultsLG;
            //ViewBag.faultsLL = faultsLL;
            //ViewBag.faultsLLL = faultsLLL;
            //ViewBag.volts = volts;
            //ViewBag.others = others;
            //ViewBag.voltsags = voltsags;
            //ViewBag.voltswells = voltswells;

            return View();
        }

        public ActionResult Faults()
        {
            m_appModel.ConfigureView(Url.RequestContext, "Faults", ViewBag);
            int groupID = m_dataContext.Connection.ExecuteScalar<int?>("Select ID From ValueListGroup Where Name = 'faultBin'") ?? 0;
            ViewBag.faultBins = m_dataContext.Table<ValueList>().QueryRecords(restriction: new RecordRestriction("GroupID = {0}", groupID)).ToArray();



            return View();
        }

        public ActionResult VoltSource()
        {
            m_appModel.ConfigureView(Url.RequestContext, "VoltSource", ViewBag);
            int groupID = m_dataContext.Connection.ExecuteScalar<int?>("Select ID From ValueListGroup Where Name = 'faultBin'") ?? 0;
            ViewBag.faultBins = m_dataContext.Table<ValueList>().QueryRecords(restriction: new RecordRestriction("GroupID = {0}", groupID)).ToArray();



            return View();
        }


        public ActionResult Help()
        {
            m_appModel.ConfigureView(Url.RequestContext, "Help", ViewBag);
            return View();
        }


        public ActionResult History()
        {
            m_appModel.ConfigureView(Url.RequestContext, "History", ViewBag);
            return View();
        }

        public ActionResult Contact()
        {
            m_appModel.ConfigureView(Url.RequestContext, "Contact", ViewBag);
            ViewBag.Message = "Contacting the Grid Protection Alliance";
            return View();
        }

        public ActionResult DisplayPDF()
        {
            // Using route ID, i.e., /Main/DisplayPDF/{id}, as page name of PDF load
            string routeID = Url.RequestContext.RouteData.Values["id"] as string ?? "UndefinedPageName";
            m_appModel.ConfigureView(Url.RequestContext, routeID, ViewBag);

            return View();
        }

        public ActionResult PageTemplate1()
        {
            m_appModel.ConfigureView(Url.RequestContext, "PageTemplate1", ViewBag);
            return View();
        }

        public ActionResult CycleDataSOEPointView()
        {
            m_dbModel.ConfigureView<Model.CycleDataSOEPointView>(Url.RequestContext, "CycleDataSOEPointView", ViewBag);

            ViewBag.VoltageStatePath = "~/Images/UpDownState/Box Set 3/";
            ViewBag.BreakerElementPath = "~/Images/PointCode/BreakerElement/";
            ViewBag.StatusElementPath = "~/Images/PointCode/StatusElement/";
            ViewBag.SimpleVoltageStatePath = "~/Images/UpDownState/Summary Set/";
            ViewBag.SimpleBreakerStatusPath = "~/Images/PointCode/Summary/";

            return View();
        }

        public ActionResult IncidentEventCycleDataView()
        {
            m_dbModel.ConfigureView<IncidentEventCycleDataView>(Url.RequestContext, "IncidentEventCycleDataView", ViewBag);
            return View();
        }

        public ActionResult OpenSEE()
        {
            string soeHighlight = Request.QueryString["SOEHighlight"] ?? "-1";
            string eventID = Url.RequestContext.RouteData.Values["id"] as string ?? "-1";
            EventInfo eventInfo = m_dbContext.Table<EventInfo>().QueryRecords(restriction: new RecordRestriction("EventID = {0}", eventID)).FirstOrDefault();

            ViewBag.PreviousEventID = -1;
            ViewBag.NextEventID = -1;
            ViewBag.EventInfo = null;
            ViewBag.Channels = Enumerable.Empty<ChannelInfo>();
            ViewBag.SOEPoints = Enumerable.Empty<CycleDataSOEPointView>();

            if ((object)eventInfo != null)
            {
                using (IDbCommand command = m_dbContext.Connection.Connection.CreateCommand())
                {
                    IDbDataParameter parameter = command.CreateParameter();
                    parameter.ParameterName = "@EventID";
                    parameter.Value = eventID;

                    command.CommandText = "GetPreviousAndNextEventIds";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(parameter);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["previd"] is int)
                                ViewBag.PreviousEventID = reader["previd"];

                            if (reader["nextid"] is int)
                                ViewBag.NextEventID = reader["nextid"];
                        }
                    }
                }

                ViewBag.EventInfo = eventInfo;
                ViewBag.Channels = m_dbContext.Table<ChannelInfo>().QueryRecords(restriction: new RecordRestriction("MeterID = {0}", eventInfo.MeterID));
            }

            m_dbModel.ConfigureView(Url.RequestContext, "OpenSEE", ViewBag);

            return View();
        }

        #endregion
    }
}