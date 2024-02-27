using MattTools.Models;
using Microsoft.UI.Xaml;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Services.Maps;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace MattTools.ViewModels
{
    public class SOAMergerViewModel
    {

        public ObservableCollection<SOAFile> SOAfiles { get; set; }
        public string FolderPath;
        private ICellStyle HeaderStyle;
        private List<ICellStyle> RowStyle = new List<ICellStyle>();

        public SOAMergerViewModel()
        {
            SOAfiles = new ObservableCollection<SOAFile>();
        }

        public async Task SelectSOA(Window window)
        {
            FolderPath = string.Empty;

            try
            {
                // Create a file picker
                var openPicker = new FileOpenPicker();

                // Retrieve the window handle (HWND) of the current WinUI 3 window.
                var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

                // Initialize the file picker with the window handle (HWND).
                WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

                // Set options for your file picker
                openPicker.ViewMode = PickerViewMode.List;
                openPicker.FileTypeFilter.Add(".xls");

                // Open the picker for the user to pick a file
                var results = await openPicker.PickMultipleFilesAsync();

                if (results == null || results.Count == 0)
                    return;

                //Add File to SOA
                foreach (var file in results)
                {
                    if (FolderPath == string.Empty)
                        FolderPath = file.Path.Replace(file.Name, String.Empty);

                    //Cabang Name
                    string cabangName = string.Empty;
                    string[] cabangNameSplit = file.Name.Split('_');

                    if (cabangNameSplit != null)
                    {
                        cabangName = cabangNameSplit[0];
                    }

                    //WorkBook
                    FileStream fs = new FileStream(file.Path, FileMode.Open, FileAccess.Read);
                    IWorkbook wb = new HSSFWorkbook(fs);
                    ISheet wbSheet = wb.GetSheetAt(0);

                    fs.Close();
                    wb.Close();

                    //Add SOA Excel to List
                    SOAfiles.Add(new SOAFile()
                    {
                        FilePath = file.Path,
                        FileName = file.Name,
                        Cabang = cabangName,
                        OriginalSheet = wbSheet
                    });
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task MergeSOA()
        {
            if (SOAfiles.Count == 0)
            {
                //mainService.CreateDialog("Error", "Add SOA to combine.");
                return;
            }

            try
            {
                HSSFWorkbook wb = new HSSFWorkbook(); //Empty Workbook
                ISheet ws = wb.CreateSheet("Sheet1");
                bool copyHeader = false;
                RowStyle.Clear();
                bool rowStyled = false;

                await Task.Delay(250);

                foreach (var soa in SOAfiles)
                {
                    soa.Sheet = soa.OriginalSheet;

                    await Task.Run(() => AddCabang(soa.Sheet, soa.Cabang));

                    if (!rowStyled)
                    {
                        await Task.Run(() => GetRowStyle(ws, soa.Sheet));
                        rowStyled = true;
                    }

                    if (!copyHeader)
                    {
                        await Task.Run(() => CopyHeader(ws, soa.Sheet));
                        copyHeader = true;
                    }


                    await Task.Run(() => CopyRows(ws, soa.Sheet));


                }

                //Write Merged
                MemoryStream mstream = new MemoryStream();
                wb.Write(mstream);

                //Save Document
                StorageFolder soaFolder = await StorageFolder.GetFolderFromPathAsync(FolderPath);
                StorageFolder mergerFolder = await soaFolder.CreateFolderAsync("Merged", CreationCollisionOption.OpenIfExists);
                StorageFile mergedFile = await mergerFolder.CreateFileAsync("MergedSOA.xls", CreationCollisionOption.ReplaceExisting);

                FileStream newFile = new FileStream(mergedFile.Path, FileMode.Create, System.IO.FileAccess.Write);
                wb.Write(newFile);
                newFile.Close();

                //Close Workbook
                wb.Close();

                //mainService.HideLoading();
                //mainService.CreateDialog("Success!", folderPath + "MergedSOA.xls");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void ConvertToXLSX(HSSFWorkbook hssfWorkbook, XSSFWorkbook xssfWorkbook)
        {
            for (int sheetIndex = 0; sheetIndex < hssfWorkbook.NumberOfSheets; sheetIndex++)
            {
                ISheet hssfSheet = hssfWorkbook.GetSheetAt(sheetIndex);
                ISheet xssfSheet = xssfWorkbook.CreateSheet(hssfSheet.SheetName);

                for (int rowIndex = 0; rowIndex <= hssfSheet.LastRowNum; rowIndex++)
                {
                    IRow hssfRow = hssfSheet.GetRow(rowIndex);
                    if (hssfRow == null) continue;

                    IRow xssfRow = xssfSheet.CreateRow(rowIndex);

                    for (int cellIndex = 0; cellIndex < hssfRow.LastCellNum; cellIndex++)
                    {
                        ICell hssfCell = hssfRow.GetCell(cellIndex);
                        if (hssfCell == null) continue;

                        ICell xssfCell = xssfRow.CreateCell(cellIndex);
                        xssfCell.CellStyle = hssfCell.CellStyle;

                        switch (hssfCell.CellType)
                        {
                            case CellType.Numeric:
                                xssfCell.SetCellValue(hssfCell.NumericCellValue);
                                break;
                            case CellType.String:
                                xssfCell.SetCellValue(hssfCell.StringCellValue);
                                break;
                            case CellType.Boolean:
                                xssfCell.SetCellValue(hssfCell.BooleanCellValue);
                                break;
                            case CellType.Error:
                                xssfCell.SetCellErrorValue(hssfCell.ErrorCellValue);
                                break;
                                // Handle other cell types if needed
                        }
                    }
                }
            }
        }
        private void CopyHeader(ISheet mainSheet, ISheet soaSheet)
        {
            try
            {
                //Copy Header
                IRow mainRow = mainSheet.CreateRow(0);
                IRow soaRow = soaSheet.GetRow(0);

                HeaderStyle = mainSheet.Workbook.CreateCellStyle();
                HeaderStyle.CloneStyleFrom(soaRow.Cells[0].CellStyle);


                for (int a = 0; a < soaRow.LastCellNum; a++)
                {
                    mainRow.CreateCell(a);
                    CloneCell(soaRow.Cells[a], mainRow.Cells[a]);
                    mainRow.Cells[a].CellStyle = HeaderStyle;
                    mainSheet.AutoSizeColumn(a);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void GetRowStyle(ISheet mainSheet, ISheet soaSheet)
        {
            RowStyle.Clear();

            IRow row = soaSheet.GetRow(1);

            for (int i = 0; i < row.LastCellNum; i++)
            {
                ICellStyle style = mainSheet.Workbook.CreateCellStyle();
                style.CloneStyleFrom(row.Cells[i].CellStyle);
                style.FillPattern = FillPattern.NoFill;

                RowStyle.Add(style);
            }
        }

        private void CopyRows(ISheet mainSheet, ISheet soaSheet)
        {
            try
            {
                int startMainRow = mainSheet.LastRowNum;

                for (int row = 1; row <= soaSheet.LastRowNum; row++)
                {

                    IRow mainRow = mainSheet.CreateRow(startMainRow + row);
                    IRow soaRow = soaSheet.GetRow(row);

                    for (int col = 0; col < soaRow.LastCellNum; col++)
                    {
                        ICell mainCell = mainRow.CreateCell(col);
                        mainCell.CellStyle = RowStyle[col];
                        CloneCell(soaRow.GetCell(col), mainCell);
                    }

                }

                GC.Collect();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void AddCabang(ISheet ws, string cabang)
        {

            if (ws != null)
            {
                //Add Column
                int totalRow = ws.LastRowNum;

                //Shifting Cell
                for (int i = 0; i <= totalRow; i++)
                {
                    IRow row = ws.GetRow(i);
                    InsertColumn(row, 1);

                    //Add Cabang Column on Numba 2
                    row.Cells[2].SetCellType(CellType.String);
                    if (i == 0)
                    {
                        row.Cells[2].SetCellValue("Cabang");
                    }
                    else
                    {
                        row.Cells[2].SetCellValue(cabang);
                    }

                }

            }
        }

        private void InsertColumn(IRow row, int index)
        {
            int totalColumn = row.LastCellNum - 1;
            for (int i = totalColumn; i > index; i--)
            {
                ICell oldCell = row.Cells[i];
                ICell newCell = row.CreateCell(i + 1, oldCell.CellType);
                CloneCell(oldCell, newCell);
                CloneCellStyle(oldCell, newCell);
            }

        }

        private void CloneCell(ICell oldCell, ICell newCell)
        {


            switch (oldCell.CellType)
            {
                case CellType.String:
                    newCell.SetCellValue(oldCell.StringCellValue);
                    break;
                case CellType.Numeric:
                    newCell.SetCellValue(oldCell.NumericCellValue);
                    break;
                case CellType.Boolean:
                    newCell.SetCellValue(oldCell.BooleanCellValue);
                    break;
                case CellType.Formula:
                    newCell.SetCellValue(oldCell.CellFormula);
                    break;
                case CellType.Error:
                    newCell.SetCellValue(oldCell.ErrorCellValue);
                    break;
                case CellType.Blank:
                case CellType.Unknown:
                    break;
            }
        }

        private void CloneCellStyle(ICell oldCell, ICell newCell)
        {
            newCell.CellStyle = oldCell.CellStyle;
        }

        private void CloneCellStyle(IWorkbook wb, ICell oldCell, ICell newCell)
        {

            ICellStyle newStyle = wb.CreateCellStyle();
            newStyle.CloneStyleFrom(oldCell.CellStyle);

            newCell.CellStyle = newStyle;
        }

    }
}
