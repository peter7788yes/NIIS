<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyRecord_AddVaccine.aspx.cs" Inherits="Vaccination_RecordM_ApplyRecord_AddVaccine" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
<section class="Content3" ng-app="MyApp" ng-controller="MyController">
<h2>新增接種疫苗</h2>
<div class="close">
    <input type="button" id="closeBtn" value="取消" class="btn" />
</div>
      <div class="formTb">
          <table>
                      <tr>
                          <td>
                           <select>
                               <option>疫苗列表</option>
                           </select>
                           <input type="button" id="addBtn" value="加入" class="btn"/>
                        </td>
                  </tr>
            </table>
       </div>
  <div class="listTb">
    <table>
  <tr>
    <th scope="col"><input type="checkbox"/></th>
    <th scope="col">批號</th>
    <th scope="col">有效日期</th>
    <th scope="col">庫存量</th>
  </tr>
     <tr  ng-repeat="record in TM.data track by $index">
                      <td class="aCenter"><input type="button" value="選擇" ng-click="goAdd(record)" class="btn" /></td>
                      <td class="aCenter" ng-bind='record["VI"]'></td>
                      <td class="aCenter" ng-bind='record["VC"]'></td>
      </tr>
</table>
</div>
</section>
     <script>
        var C =<%:CaseUserID %>;
        var tbAry =<%=tbAry%>;
    </script>
</asp:Content>
