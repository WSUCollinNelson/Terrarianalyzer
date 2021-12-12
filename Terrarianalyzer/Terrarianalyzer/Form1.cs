using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Terrarianalyzer
{
    public partial class Form1 : Form
    {
        private WorldObject storedWorld;
        public Form1(WorldObject world)
        {
            storedWorld = world;
            InitializeComponent();
            DisplayTileDataOnChart(world, "Dirt");

            comboBox1.Items.AddRange(XMLUtilities.GetAllTileNames());
            comboBox2.Items.AddRange(XMLUtilities.GetAllItemNames());
            totalTilesLabel.Text = world.Tiles.Count.ToString();
            totalChestsLabel.Text = world.Chests.Count.ToString();

            SetTotalWater();
            SetTotalLava();
            SetTotalHoney();

            CreateTileChart(world);
            CreateItemChart(world);
        }

        private void DisplayTileDataOnChart(WorldObject world, string tileTarget)
        {
            int[] itemCount = WorldMiner.CountTilesByDepth(world, XMLUtilities.GetTileID(tileTarget));

            ChartArea chartArea1 = new ChartArea();
            Series series1 = new Series();
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.Legends.Clear();

            chartArea1.Name = "ChartArea";
            this.chart1.ChartAreas.Add(chartArea1);

            this.chart1.Name = "tileChart";
            series1.ChartArea = "ChartArea";
            series1.Legend = "Legend";
            series1.Name = tileTarget;
            series1.ChartType = SeriesChartType.SplineRange;


            for (int i = 0; i < itemCount.Length; i++)
            {
                series1.Points.AddXY(i, itemCount[i]);
            }

            this.chart1.Series.Add(series1);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "depthChart";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayTileDataOnChart(storedWorld, comboBox1.SelectedItem.ToString());
        }

        private void SetTotalWater()
        {
            int totalAmount = 0;
            foreach(TileObject tile in storedWorld.Tiles)
            {
                if(tile.LiquidType == LiquidType.Water)
                {
                    totalAmount += tile.LiquidAmount;
                }
            }
            totalWaterLabel.Text = (totalAmount / 100).ToString();
        }

        private void SetTotalLava()
        {
            int totalAmount = 0;
            foreach (TileObject tile in storedWorld.Tiles)
            {
                if (tile.LiquidType == LiquidType.Lava)
                {
                    totalAmount += tile.LiquidAmount;
                }
            }
            totalLavaLabel.Text = (totalAmount / 100).ToString();
        }

        private void SetTotalHoney()
        {
            int totalAmount = 0;
            foreach (TileObject tile in storedWorld.Tiles)
            {
                if (tile.LiquidType == LiquidType.Honey)
                {
                    totalAmount += tile.LiquidAmount;
                }
            }
            totalHoneyLabel.Text = (totalAmount / 100).ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            StringBuilder chestsFound = new StringBuilder();
            int selectedItemID = XMLUtilities.GetItemID((string)comboBox2.SelectedItem);
            bool foundSome = false;
            foreach(ChestObject chest in storedWorld.Chests)
            {
                if(chest.ChestItems.Exists(x => x.ItemID == selectedItemID))
                {
                    chestsFound.AppendLine($"Item found in chest at {chest.Position}");
                    foundSome = true;
                }
            }

            if (!foundSome)
            {
                chestsFound.AppendLine("Item not found in any chests...");
            }
            textBox1.Text = chestsFound.ToString();
            DisplayChestDataOnChart(storedWorld, (string)comboBox2.SelectedItem);
        }

        private void DisplayChestDataOnChart(WorldObject world, string itemTarget)
        {
            ChartArea chartArea1 = new ChartArea();
            Series series1 = new Series();
            chart2.Series.Clear();
            chart2.ChartAreas.Clear();
            chart2.Legends.Clear();

            chartArea1.Name = "ChartArea";
            chartArea1.AxisX.Minimum = 0;
            chartArea1.AxisX.Maximum = 8400;
            chartArea1.AxisY.Minimum = 0;
            chartArea1.AxisY.Maximum = 2400;
            chartArea1.AxisY.IsReversed = true;
            this.chart2.ChartAreas.Add(chartArea1);

            this.chart2.Name = "chestPlot";
            series1.ChartArea = "ChartArea";
            series1.Name = itemTarget;
            series1.ChartType = SeriesChartType.Point;


            int selectedItemID = XMLUtilities.GetItemID(itemTarget);
            foreach (ChestObject chest in storedWorld.Chests)
            {
                if (chest.ChestItems.Exists(x => x.ItemID == selectedItemID))
                {
                    series1.Points.AddXY(chest.Position.Item1, chest.Position.Item2);
                }
            }

            this.chart2.Series.Add(series1);
            this.chart2.TabIndex = 0;
            this.chart2.Text = "chestPlot";
        }

        private void CreateTileChart(WorldObject world)
        {
            ChartArea chartArea1 = new ChartArea();
            Series series1 = new Series();
            chart3.Series.Clear();
            chart3.ChartAreas.Clear();
            chart3.Legends.Clear();

            chartArea1.Name = "ChartArea";
            this.chart3.ChartAreas.Add(chartArea1);

            this.chart3.Name = "tileChart";
            series1.ChartArea = "ChartArea";
            series1.Name = "tiles";
            series1.ChartType = SeriesChartType.Pie;
            series1.SetCustomProperty("PieLabelStyle", "Outside");

            Dictionary<int, int> tileCounts = new Dictionary<int, int>();
            foreach (TileObject tile in storedWorld.Tiles)
            {
                if(tileCounts.ContainsKey(tile.TypeID))
                {
                    tileCounts[tile.TypeID]++;
                }
                else
                {
                    tileCounts.Add(tile.TypeID, 1);
                }
            }

            int otherCount = 0;
            foreach(int key in tileCounts.Keys)
            {
                try
                {
                    if (tileCounts[key] > 200000)
                    {
                        series1.Points.AddXY(XMLUtilities.GetTileName(key), tileCounts[key]);
                    }
                    else
                    {
                        otherCount += tileCounts[key];
                    }
                }
                catch
                {

                }
            }

            series1.Points.AddXY("Other", otherCount);

            this.chart3.Series.Add(series1);
            this.chart3.TabIndex = 0;
            this.chart3.Text = "chestPlot";
        }

        private void CreateItemChart(WorldObject world)
        {
            ChartArea chartArea1 = new ChartArea();
            Series series1 = new Series();
            chart4.Series.Clear();
            chart4.ChartAreas.Clear();
            chart4.Legends.Clear();

            chartArea1.Name = "ChartArea";
            this.chart4.ChartAreas.Add(chartArea1);

            this.chart4.Name = "itemChart";
            series1.ChartArea = "ChartArea";
            series1.Name = "tiles";
            series1.ChartType = SeriesChartType.Pie;
            series1.SetCustomProperty("PieLabelStyle", "Outside");

            Dictionary<int, int> itemCounts = new Dictionary<int, int>();
            foreach (ChestObject chest in storedWorld.Chests)
            {
                foreach (ItemObject item in chest.ChestItems)
                {
                    if (itemCounts.ContainsKey(item.ItemID))
                    {
                        itemCounts[item.ItemID]++;
                    }
                    else
                    {
                        itemCounts.Add(item.ItemID, 1);
                    }
                }
            }

            int otherCount = 0;
            foreach (int key in itemCounts.Keys)
            {
                try
                {
                    if (itemCounts[key] > 100)
                    {
                        series1.Points.AddXY(XMLUtilities.GetItemName(key), itemCounts[key]);
                    }
                    else if(itemCounts[key] > 50)
                    {
                        series1.Points.AddXY("", itemCounts[key]);
                    }
                    else
                    {
                        otherCount += itemCounts[key];
                    }
                }
                catch
                {

                }
            }

            series1.Points.AddXY("Other", otherCount);

            this.chart4.Series.Add(series1);
            this.chart4.TabIndex = 0;
            this.chart4.Text = "chestPlot";
        }
    }
}
