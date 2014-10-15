﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Usuario.ascx.cs" Inherits="Votaai.UserControl.Usuario" %>

<div class="tab-pane" id="Formusuario">
    <div id="edit-profile" class="form-horizontal">
        <fieldset>

            <div class="control-group">
                <label class="control-label" for="username">Login</label>
                <div class="controls">
                    <input type="text" class="span4" id="login" name="login" placeholder="Login">
                </div>
                <!-- /controls -->
            </div>
            <!-- /control-group -->

            <div class="control-group">
                <label class="control-label" for="email">Senha</label>
                <div class="controls">
                    <input type="password" class="span4" id="senha" placeholder="Senha">
                </div>
                <!-- /controls -->
            </div>
            <!-- /control-group -->

            <div class="control-group">
                <label class="control-label" for="email">Repita a senha</label>
                <div class="controls">
                    <input type="password" class="span4" id="senharepitida" name="senharepitida" placeholder="Repita a senha">
                </div>
                <!-- /controls -->
            </div>
            <!-- /control-group -->

            <div class="form-actions">
                <asp:Button runat="server" ID="BtnCadUsu" CssClass="btn btn-success" Text="Cadastrar" OnClick="BtnCadUsu_Click" />
                <asp:Button runat="server" ID="BtnCanUsu" CssClass="btn" Text="Cancelar" OnClick="BtnCanUsu_Click"></asp:Button>
            </div>
            <!-- /form-actions -->

        </fieldset>
    </div>
</div>

