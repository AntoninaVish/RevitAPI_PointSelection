using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPI_PointSelection
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand

    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            //создаем переменную 
            XYZ pickedPoint = null;

            //блок try catch обрабатывает исключения
            try
            {
                //обращаемся к uidoc, классу selection, вызываем метод PickedPoint
                //указываем ObjectSnapTypes - это точка с учетом привязки, Endpoints - это по конечным точкам
                pickedPoint = uidoc.Selection.PickPoint(ObjectSnapTypes.Endpoints, "Выберите точку");
            }
            //исключение мы видели в окне в Revit при нажатии кнопки Esc
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)//этот блок оставляем пустым
            {

            }
            if (pickedPoint == null) //если pickedPoint не равен ни чему
                return Result.Cancelled; //возвращаем отмену
                       
            //обращаемся к TaskDialog и выводим координаты точки
            TaskDialog.Show("Point info", $"X: {pickedPoint.X}, Y: {pickedPoint.Y}, Z: {pickedPoint.Z}");
           
            return Result.Succeeded;
        }
    }
}
