﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Votaai
{
    public partial class Relatorio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Irá pegar o parâmetro passado pela URL
            string parametro = Request.QueryString["filtro"];

            //Faz um switch 
            switch (parametro) 
            { 
                case "estado":

                    break;
            }
        }

        protected void BtnGerarRelatorio_Click(object sender, EventArgs e)
        {

        }
    }
}