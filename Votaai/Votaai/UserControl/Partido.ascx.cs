﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Votaai.UserControl
{
    public partial class Partido : System.Web.UI.UserControl
    {
        #region Ações Tela

        /// <summary>
        /// Evento de load da página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Evento de botão de cancelar, limpando os campos da tela
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCanPart_Click(object sender, EventArgs e)
        {
            LimpaTela();
        }

        /// <summary>
        /// Evento de botão de cadastro de partido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCadPart_Click(object sender, EventArgs e)
        {
            try
            {

                ClassesBanco.Partido part = new ClassesBanco.Partido();

                if (this.hiddenpartido.Value == "")
                {
                    if (!ValidaPartExistente())
                    {
                        MontaValoresInclusao(part);
                        ValidaOperacao(ref part);
                    }
                    else
                    {
                        throw new Exception("Já existe partido com os dados informados!");
                    }
                }
                else
                {
                    MontaValoresInclusao(part);
                    ValidaOperacao(ref part);
                }

                LimpaTela();
                RegistraAlerta("Seus Dados Foram Salvos Com Sucesso!", "le-sucess", "LblSucess");

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("formato incorreto"))
                {
                    RegistraAlerta("Dados informados incorretamente! Gentileza verificar!", "le-alert", "lbldanger");

                }

                else
                {
                    RegistraAlerta(ex.Message.ToString(), "le-alert", "lbldanger");

                }
            }
        }

        /// <summary>
        /// Evento de pesquisar partido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                ClassesBanco.Partido part = new ClassesBanco.Partido();
                part.sigla = this.pessigla.Value;

                DataSet dados = part.BuscarDados(part);

                this.cpnjpartido.Value = dados.Tables[0].Rows[0]["cnpj"].ToString();
                this.nomepartido.Value = dados.Tables[0].Rows[0]["nome"].ToString();
                this.siglapartido.Value = dados.Tables[0].Rows[0]["sigla"].ToString();
                this.prefixopartido.Text = dados.Tables[0].Rows[0]["prefixo"].ToString();
                this.hiddenpartido.Value = dados.Tables[0].Rows[0]["partidoid"].ToString();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        /// <summary>
        /// Evento de registrar alerta para mensagens ao usuário
        /// </summary>
        /// <param name="msgalerta"></param>
        /// <param name="nomediv"></param>
        /// <param name="nomelabel"></param>
        private void RegistraAlerta(string msgalerta, string nomediv, string nomelabel)
        {
            ((Cadastros)this.Page).RegistraAlerta(msgalerta, nomediv, nomelabel);
        }

        /// <summary>
        /// Verifica se existe algum partido existente com o CNPJ, ou nome, ou prefixo ou a sigla
        /// </summary>
        /// <returns></returns>
        private bool ValidaPartExistente()
        {
            ClassesBanco.Partido validapart = new ClassesBanco.Partido();
            validapart.cnpj = this.cpnjpartido.Value;
            validapart.nome = this.nomepartido.Value;
            if (this.prefixopartido.Text == "")
            {
                throw new Exception("Prefixo do partido não informado!");
            }
            else
            {
                validapart.prefixo = int.Parse(this.prefixopartido.Text);

            }
            validapart.sigla = this.siglapartido.Value;

            DataSet dados = validapart.BuscarDadosAlteracao(validapart);
            if (dados.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Monta os valores da tela no objeto para inclusão ou alteração
        /// </summary>
        /// <param name="part"></param>
        private void MontaValoresInclusao(ClassesBanco.Partido part)
        {
            try
            {
                part.cnpj = this.cpnjpartido.Value;
                part.nome = this.nomepartido.Value.ToString();
                part.sigla = this.siglapartido.Value;
                part.prefixo = int.Parse(this.prefixopartido.Text);

                if (part.cnpj == "")
                {
                    throw new Exception("CNPJ não informado!");
                }

                if (part.nome == "")
                {
                    throw new Exception("Nome do partido não informado!");
                }

                if (part.sigla == "")
                {
                    throw new Exception("Sigla não informada!");
                }
                if (part.prefixo == 0)
                {
                    throw new Exception("Número do partido inválido ou não informado!");
                }
                if (this.hiddenpartido.Value != "")
                {
                    part.partidoid = int.Parse(this.hiddenpartido.Value);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Limpa os campos da tela
        /// </summary>
        private void LimpaTela()
        {
            this.cpnjpartido.Value = "";
            this.nomepartido.Value = "";
            this.siglapartido.Value = "";
            this.prefixopartido.Text = "";
            this.pessigla.Value = "";
        }

        /// <summary>
        /// Valida qual operação será executada no banco de dados, I - Inclusão, A - Alteração
        /// </summary>
        /// <param name="part"></param>
        private void ValidaOperacao(ref ClassesBanco.Partido part)
        {
            if (this.hiddenpartido.Value == "")
            {
                part.ExecutarMetodo('I', part);
            }
            else
            {
                part.ExecutarMetodo('A', part);
            }
        }

    }
}