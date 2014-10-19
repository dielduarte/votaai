﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Votaai.UserControl
{
    public partial class Candidato : System.Web.UI.UserControl
    {
        #region Ações Tela

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaComboPartido();
            }

        }

        protected void BtnCadCand_Click(object sender, EventArgs e)
        {
            ClassesBanco.Candidato cand = new ClassesBanco.Candidato();
            cand.nome = nomecandidato.Value.ToString();
            cand.numero = int.Parse(numero.Text);
            cand.cargo = this.selectcargo.Value;

            ValidarFoto(ref cand);
            ValidarVice(ref cand);

            cand.partidoid = int.Parse(this.selectpartido.SelectedValue);

            ValidaOperacao(ref cand);
        }

        protected void BtnCanCand_Click(object sender, EventArgs e)
        {

        }

        protected void BtnPesquisar_Click(object sender, EventArgs e)
        {

        }

        protected void selectpartido_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (selectpartido.DataSource==null)
            {
                selectpartido.DataSource = Session["DadosCombo"];
                
            }
            DataSet valoratual = (DataSet)selectpartido.DataSource;
            
            if (this.selectpartido.SelectedIndex > 0)
            {
                DataRow[] row = valoratual.Tables[0].Select(string.Format("partidoid={0}", this.selectpartido.SelectedValue));
                this.numero.Text = row[0].ItemArray[4].ToString();

            }
            else
            {
                this.numero.Text = "";
            }
        }
        #endregion

        #region Montagem de Combo

        private void CarregaComboPartido()
        {
            ClassesBanco.Partido part = new ClassesBanco.Partido();
            DataSet dados = part.BuscarDados(part);

            MontaComboPartido(dados);
        }

        private void MontaComboPartido(DataSet dados)
        {
            DataRow row = dados.Tables[0].NewRow();

            row[0] = 0;
            row[4] = "";
            dados.Tables[0].Rows.InsertAt(row, 0);

            this.selectpartido.DataSource = dados;
            this.selectpartido.DataTextField = "sigla";
            this.selectpartido.DataValueField = "partidoid";
            this.selectpartido.DataBind();
            Session["DadosCombo"] = dados;
        }
        #endregion

        #region Validações para Cadastro

        private void ValidarVice(ref ClassesBanco.Candidato cand)
        {
            if (cand.cargo == "1" || cand.cargo == "3")
            {
                cand.vice = txtvice.Value;
            }
            else if (cand.cargo == "2")
            {
                cand.vice = this.txtsuplente1.Value + ";" + this.txtsuplente2.Value;
            }
            else
            {
                cand.vice = null;
            }
        }

        private void ValidarFoto(ref ClassesBanco.Candidato cand)
        {
            string filepath = Server.MapPath("~/ImagensCandidatos/");
            string fullpath = filepath + FileFotoCand.FileName;
            this.FileFotoCand.SaveAs(fullpath);
            cand.foto = fullpath;
        }

        private void ValidaOperacao(ref ClassesBanco.Candidato cand)
        {
            if (this.hiddencand.Value == "")
            {
                cand.ExecutarMetodo('I', cand);
            }
            else
            {
                cand.ExecutarMetodo('A', cand);
            }
        }
        #endregion

    }
}