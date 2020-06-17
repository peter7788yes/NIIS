﻿using NPOI.HSSF.Record.Crypto;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Class1 的摘要描述
/// </summary>
public class BDiseaseExportExcelUtil
{
    public HSSFWorkbook workbook;
    private ISheet worksheet;
    //private DataTable data;
    private string fileName;
    //private ArrayList tableHeader;
    
    public int columnSize;
    public int rowCount = 0;

    public ICellStyle headerCenterStyle;
    public ICellStyle headerLeftStyle;
    public ICellStyle headerRightStyle;
    public ICellStyle tableHeaderStyle;
    public ICellStyle tableDataStyle;

    public BDiseaseExportExcelUtil(string fileName, int columnSize)
    {

        //FileStream fs = new FileStream(System.Web.Hosting.HostingEnvironment.MapPath("~/template.xls"), FileMode.Open, FileAccess.ReadWrite);
        //workbook = new HSSFWorkbook(fs);
        workbook = new HSSFWorkbook();
        //fs.Close();

        this.fileName = fileName;
        //this.tableHeader = tableHeader;
        //this.data = data;
        this.columnSize = columnSize;
        
        IFont headerFont = workbook.CreateFont();
        headerFont.Boldweight = (short)FontBoldWeight.Bold;
        headerFont.FontHeightInPoints = 12;

        IFont dataFont = workbook.CreateFont();
        dataFont.FontHeightInPoints = 12;

        headerCenterStyle = workbook.CreateCellStyle();
        headerCenterStyle.Alignment = HorizontalAlignment.Center;
        headerCenterStyle.SetFont(headerFont);

        headerLeftStyle = workbook.CreateCellStyle();
        headerLeftStyle.SetFont(headerFont);

        headerRightStyle = workbook.CreateCellStyle();
        headerRightStyle.Alignment = HorizontalAlignment.Right;
        headerRightStyle.SetFont(headerFont);

        tableHeaderStyle = workbook.CreateCellStyle();
        tableHeaderStyle.WrapText = true;
        tableHeaderStyle.Alignment = HorizontalAlignment.Center;
        tableHeaderStyle.VerticalAlignment = VerticalAlignment.Center;
        tableHeaderStyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;
        tableHeaderStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
        tableHeaderStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        tableHeaderStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        tableHeaderStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        tableHeaderStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        tableHeaderStyle.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        tableHeaderStyle.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        tableHeaderStyle.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        tableHeaderStyle.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        tableHeaderStyle.SetFont(headerFont);

        tableDataStyle = workbook.CreateCellStyle();
        tableDataStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        tableDataStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        tableDataStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        tableDataStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        tableDataStyle.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        tableDataStyle.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        tableDataStyle.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        tableDataStyle.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        tableDataStyle.SetFont(dataFont);

        worksheet = workbook.CreateSheet(fileName);
        
        //指定直式或橫式 true=橫式 false=直式
        worksheet.PrintSetup.Landscape = true;
        //指定紙張大小 A3=8, A4=9, Letter=1
        worksheet.PrintSetup.PaperSize = 9;
        //設定邊距置中
        worksheet.HorizontallyCenter = true;
        //sheet.VerticallyCenter = true;
        //設定一頁寬
        worksheet.Autobreaks = true;
        worksheet.PrintSetup.FitHeight = (short)0;
        worksheet.PrintSetup.FitWidth = (short)1;

        //Water Print
        worksheet.Header.Center = "&50 &K808080 \n\n\n &D &T " + AuthServer.GetLoginUser().UserName;
        worksheet.ProtectSheet(AuthServer.GetLoginUser().LoginName);
        
    }

    public void CopyRow(HSSFWorkbook workbook, ISheet worksheet, int sourceRowNum, int destinationRowNum)
    {
        // Get the source / new row
        IRow newRow = worksheet.GetRow(destinationRowNum);
        IRow sourceRow = worksheet.GetRow(sourceRowNum);

        // If the row exist in destination, push down all rows by 1 else create a new row
        if (newRow != null)
        {
            worksheet.ShiftRows(destinationRowNum, worksheet.LastRowNum, 1);
        }
        else
        {
            newRow = worksheet.CreateRow(destinationRowNum);
        }

        // Loop through source columns to add to new row
        for (int i = 0; i < sourceRow.LastCellNum; i++)
        {
            // Grab a copy of the old/new cell
            ICell oldCell = sourceRow.GetCell(i);
            ICell newCell = newRow.CreateCell(i);

            // If the old cell is null jump to next cell
            if (oldCell == null)
            {
                newCell = null;
                continue;
            }

            // Copy style from old cell and apply to new cell
            ICellStyle newCellStyle = workbook.CreateCellStyle();
            newCellStyle.CloneStyleFrom(oldCell.CellStyle);            
            newCell.CellStyle = newCellStyle;

            // If there is a cell comment, copy
            if (newCell.CellComment != null) newCell.CellComment = oldCell.CellComment;

            // If there is a cell hyperlink, copy
            if (oldCell.Hyperlink != null) newCell.Hyperlink = oldCell.Hyperlink;

            // Set the cell data type
            newCell.SetCellType(oldCell.CellType);

            // Set the cell data value
            switch (oldCell.CellType)
            {
                case CellType.Blank:
                    newCell.SetCellValue(oldCell.StringCellValue);
                    break;
                case CellType.Boolean:
                    newCell.SetCellValue(oldCell.BooleanCellValue);
                    break;
                case CellType.Error:
                    newCell.SetCellErrorValue(oldCell.ErrorCellValue);
                    break;
                case CellType.Formula:
                    newCell.SetCellFormula(oldCell.CellFormula);
                    break;
                case CellType.Numeric:
                    newCell.SetCellValue(oldCell.NumericCellValue);
                    break;
                case CellType.String:
                    newCell.SetCellValue(oldCell.RichStringCellValue);
                    break;
                case CellType.Unknown:
                    newCell.SetCellValue(oldCell.StringCellValue);
                    break;
            }
            
        }

        // If there are are any merged regions in the source row, copy to new row
        for (int i = 0; i < worksheet.NumMergedRegions; i++)
        {
            CellRangeAddress cellRangeAddress = worksheet.GetMergedRegion(i);
            if (cellRangeAddress.FirstRow == sourceRow.RowNum)
            {
                CellRangeAddress newCellRangeAddress = new CellRangeAddress(newRow.RowNum,
                                                                            (newRow.RowNum +
                                                                             (cellRangeAddress.FirstRow -
                                                                              cellRangeAddress.LastRow)),
                                                                            cellRangeAddress.FirstColumn,
                                                                            cellRangeAddress.LastColumn);
                worksheet.AddMergedRegion(newCellRangeAddress);
            }
        }
    }

    public void insertReportHeaderCenter(string cellText)
    {
        ICell cell = worksheet.CreateRow(rowCount).CreateCell(0);
        cell.CellStyle = headerCenterStyle;
        cell.SetCellValue(cellText);
        worksheet.AddMergedRegion(new CellRangeAddress(rowCount, rowCount, 0, columnSize));
        rowCount++;
    }
    public void insertReportHeaderLeftRight(string leftText, string rightText)
    {
        ICell cell = worksheet.CreateRow(rowCount).CreateCell(0);
        cell.CellStyle = headerLeftStyle;
        cell.SetCellValue(leftText);
        worksheet.AddMergedRegion(new CellRangeAddress(rowCount, rowCount, 0, columnSize / 2));

        cell = worksheet.GetRow(rowCount).CreateCell(columnSize / 2 + 1);
        cell.CellStyle = headerRightStyle;
        cell.SetCellValue(rightText);
        worksheet.AddMergedRegion(new CellRangeAddress(rowCount, rowCount, columnSize / 2 + 1, columnSize));

        rowCount++;
    }

    public void insertReportHeaderCenter(string cellText, int rowCount)
    {
        ICell cell = worksheet.CreateRow(rowCount).CreateCell(0);
        cell.CellStyle = headerCenterStyle;
        cell.SetCellValue(cellText);
        worksheet.AddMergedRegion(new CellRangeAddress(rowCount, rowCount, 0, columnSize));
    }

    public void insertReportHeaderLeftRight(string leftText, string rightText, int rowCount)
    {
        ICell cell = worksheet.CreateRow(rowCount).CreateCell(0);
        cell.CellStyle = headerLeftStyle;
        cell.SetCellValue(leftText);
        worksheet.AddMergedRegion(new CellRangeAddress(rowCount, rowCount, 0, columnSize / 2));

        cell = worksheet.GetRow(rowCount).CreateCell(columnSize / 2 + 1);
        cell.CellStyle = headerRightStyle;
        cell.SetCellValue(rightText);
        worksheet.AddMergedRegion(new CellRangeAddress(rowCount, rowCount, columnSize / 2 + 1, columnSize));
    }


    public void insertReportHeaderLeft(string cellText)
    {
        ICell cell = worksheet.CreateRow(rowCount).CreateCell(0);
        cell.CellStyle = headerLeftStyle;
        cell.SetCellValue(cellText);
        worksheet.AddMergedRegion(new CellRangeAddress(rowCount, rowCount, 0, columnSize / 2));
        rowCount++;
    }
    public void insertReportHeaderRight(string cellText)
    {
        ICell cell = worksheet.CreateRow(rowCount).CreateCell(columnSize / 2 + 1);
        cell.CellStyle = headerRightStyle;
        cell.SetCellValue(cellText);
        worksheet.AddMergedRegion(new CellRangeAddress(rowCount, rowCount, columnSize / 2 + 1, columnSize));
        rowCount++;
    }

    public static string CalcDivide(Double a, Double b)
    {
        if (a == 0 || b == 0)
        {
            return "0%";
        }
        else
        {
            return Math.Round(a / b * 100, 2, MidpointRounding.AwayFromZero) + "%";
        }
    }

    public static string CalcDivide(string x, string y)
    {
        Double a = Convert.ToDouble(x);
        Double b = Convert.ToDouble(y);
        if (a == 0 || b == 0)
        {
            return "0%";
        }
        else
        {
            return Math.Round(a / b * 100, 2, MidpointRounding.AwayFromZero) + "%";
        }
    }

    public static string CalcDivide(object x, object y)
    {
        Double a = Convert.ToDouble(x);
        Double b = Convert.ToDouble(y);
        if (a == 0 || b == 0)
        {
            return "0%";
        }
        else
        {
            return Math.Round(a / b * 100, 2, MidpointRounding.AwayFromZero) + "%";
        }
    }

    public static string CalcDivideNonPercent(object x, object y)
    {
        if (x == null || y == null) return "0";
        if (x == DBNull.Value || y == DBNull.Value) return "0";
        Double a = Convert.ToDouble(x);
        Double b = Convert.ToDouble(y);
        if (a == 0 || b == 0)
        {
            return "0";
        }
        else
        {
            return Math.Round(a / b * 100, 2, MidpointRounding.AwayFromZero).ToString();
        }
    }


    public static string CalcSum(object a, object b)
    {
        int x = Convert.ToInt32(a);
        int y = Convert.ToInt32(b);
        return Convert.ToString(x + y);
    }

    public static string CalcSum(string a, string b)
    {
        int x = Convert.ToInt32(a);
        int y = Convert.ToInt32(b);
        return Convert.ToString(x + y);
    }

    /*
    public MemoryStream export()
    {
        #region Table Header
        
        foreach (List<KeyValuePair<string, int[]>> tableHeaderList in tableHeader)
        {
            IRow tableHeaderRow = worksheet.CreateRow(rowCount);
            int initCellPosition = 0;
            for(int i = 0; i< tableHeaderList.Count; i++)
            {
                KeyValuePair<string, int[]> pair = tableHeaderList.ElementAt(i);
                int[] range = pair.Value;
                ICell cell = tableHeaderRow.CreateCell(i + initCellPosition);
                cell.SetCellValue(pair.Key);                
                worksheet.AddMergedRegion(new CellRangeAddress(rowCount, rowCount + range[0], i + initCellPosition, i + initCellPosition + range[1]));
                cell.CellStyle = tableHeaderStyle;
                initCellPosition = range[1];
            }            
            rowCount++;

        }
        #endregion

        #region rowdata
        for (int i = 0; i < data.Rows.Count; i++)
        {
            IRow row = worksheet.CreateRow(++rowCount);
            for (int j = 0; j < data.Columns.Count; j++)
            {
                row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
            }
        }
        #endregion

        MemoryStream ms = new MemoryStream();
        workbook.Write(ms);
        workbook = null;
        return ms;

    }
    */
}