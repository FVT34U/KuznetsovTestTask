using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;

namespace TestTask;

public class DatabaseController
{
    public ObservableCollection<Node> GetDataAsList()
    {
        var nodes = new ObservableCollection<Node>();

        using (ApplicationContext db = new ApplicationContext())
        {
            var components = db.EngineComponents.ToList();

            foreach (var component in components)
            {
                nodes.Add(component.ToNode());
            }
        }

        foreach (var childNode in nodes.OrderByDescending(x => x.NestingLevel))
        {
            var id = childNode.ParentId;
            
            if (id == null) continue;
            
            foreach (var parentNode in nodes)
            {
                if (parentNode.ID != id) continue;

                parentNode.Nodes?.Add(childNode);
            }

            nodes.Remove(childNode);
        }

        return nodes;
    }

    public void AddEngine()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var components = db.EngineComponents.ToList().FindAll(x => x.NestingLevel == 0);
            var lastEngineIndex = components.MaxBy(x => x.Value).Value;
                
            var component = new EngineComponent
            {
                Name = "Двигатель",
                Value = lastEngineIndex + 1,
                ParentId = null,
                NestingLevel = 0
            };
                
            db.EngineComponents.Add(component);
            db.SaveChanges();
        }
    }

    public void DeleteComponent(Node nodeToDelete)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var curNodes = nodeToDelete.Nodes;

            while (true)
            {
                if (curNodes.Count == 0) break;

                foreach (var node in curNodes.ToList())
                {
                    if (node.Nodes.Count != 0)
                    {
                        foreach (var subNode in node.Nodes.ToList())
                        {
                            curNodes.Add(subNode);
                        }
                    }

                    curNodes.Remove(node);
                    
                    db.EngineComponents.Remove(node.ToEngineComponent());
                    db.SaveChanges();
                }
            }

            db.EngineComponents.Remove(nodeToDelete.ToEngineComponent());
            db.SaveChanges();
        }
    }

    public bool RenameComponent(Node nodeToRename, string name)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            if (db.EngineComponents.ToList().FindAll(x => x.Name == name).Count != 0) return false;
            
            var compsWithSameName = db.EngineComponents.ToList().FindAll(x => x.Name == nodeToRename.GetTrueName());

            foreach (var comp in compsWithSameName)
            {
                comp.Name = name;

                db.EngineComponents.Update(comp);
                db.SaveChanges();
            }
        }

        return true;
    }

    public void AddComponent(Node parentNode, string name, int value)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.EngineComponents.Add(new EngineComponent
            {
                Name = name,
                Value = value,
                ParentId = parentNode.ID,
                NestingLevel = parentNode.NestingLevel + 1
            });
            db.SaveChanges();
        }
    }

    public void ShowLog(Node nodeToShow, string path)
    {
        var date = DateTime.Now.ToString();
        date = date.Replace(":", "-");
        date = date.Replace(".", "-");
        date = date.Replace(" ", "_");

        var filename = path + "\\" + nodeToShow.GetTrueName() + "_" + date;

        var app = new Word.Application();
        var doc = app.Documents.Add(Visible:false);

        var curNodes = nodeToShow.Nodes;

        var nodesToShow = new List<Node>();

        while (true)
        {
            if (curNodes.Count == 0) break;

            foreach (var node in curNodes.ToList())
            {
                if (node.Nodes.Count != 0)
                {
                    foreach (var subNode in node.Nodes.ToList())
                    {
                        curNodes.Add(subNode);
                    }
                }

                nodesToShow.Add(node);

                curNodes.Remove(node);
            }
        }

        var range = doc.Range();
        var table = doc.Tables.Add(range, NumColumns:2, NumRows: nodesToShow.Count);
        table.Borders.Enable = 1;

        var enumerator = nodesToShow.GetEnumerator();
        enumerator.MoveNext();

        foreach (Word.Row row in table.Rows)
        {
            foreach (Word.Cell cell in row.Cells)
            {
                if (cell.ColumnIndex == 1) cell.Range.Text = enumerator.Current.GetTrueName();
                if (cell.ColumnIndex == 2) cell.Range.Text = enumerator.Current.Value.ToString() + " шт";
            }
            if (!enumerator.MoveNext()) break;
        }

        try { doc.SaveAs(FileName: filename, FileFormat: Word.WdSaveFormat.wdFormatDocumentDefault); }
        catch { MessageBox.Show("Ошибка в сохранении файла!"); }

        try { doc.Close(); app.Quit(); }
        catch { MessageBox.Show("Ошибка при остановке работы MS Word!"); }
    }
}