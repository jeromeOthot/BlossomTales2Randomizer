using BlossomTales2Randomizer.Models;

namespace BlossomTales2Randomizer;

public sealed class MainForm : Form
{
    private const string DefaultStatusText = "Hover over an option to see a description of what it does.";
    private const int GridColumns = 4;

    private readonly List<SettingDefinition> _catalog;
    private readonly Dictionary<string, bool> _values;
    private readonly Dictionary<string, CheckBox> _checkboxes = new();
    private readonly Dictionary<string, (Label Header, List<SettingDefinition> Items)> _groupHeaders = new();
    private readonly Label _statusLabel;

    public MainForm()
    {
        _catalog = SettingsCatalog.Definitions;
        _values = SettingsStore.Load(_catalog);

        Text = "Blossom Tales 2 Randomizer";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(760, 480);
        ClientSize = new Size(900, 560);
        Font = new Font("Segoe UI", 9F);

        _statusLabel = new Label
        {
            AutoSize = true,
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            Text = DefaultStatusText,
            ForeColor = SystemColors.GrayText,
            Padding = new Padding(4, 6, 4, 6),
        };

        var tabs = new TabControl { Dock = DockStyle.Fill };
        var tabPage = new TabPage("Settings");
        tabPage.Controls.Add(BuildGroupsHost());
        tabs.TabPages.Add(tabPage);

        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 3,
            Padding = new Padding(10),
        };
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.Controls.Add(tabs, 0, 0);
        root.Controls.Add(_statusLabel, 0, 1);
        root.Controls.Add(BuildBottomBar(), 0, 2);

        Controls.Add(root);
    }

    private Control BuildGroupsHost()
    {
        var host = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            AutoScroll = true,
            Padding = new Padding(10),
        };
        host.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        var categories = _catalog.Select(d => d.Category).Distinct().ToList();
        host.RowCount = categories.Count * 3;

        for (var g = 0; g < categories.Count; g++)
        {
            var category = categories[g];
            var items = _catalog.Where(d => d.Category == category).ToList();

            var headerLabel = new Label
            {
                Text = FormatHeader(category, items),
                Font = new Font(Font, FontStyle.Bold),
                AutoSize = true,
                Margin = new Padding(0, g == 0 ? 0 : 14, 0, 2),
            };
            _groupHeaders[category] = (headerLabel, items);

            var ruleLine = new Panel
            {
                Height = 1,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = SystemColors.ControlDark,
                Margin = new Padding(0, 0, 0, 6),
            };

            var grid = BuildCheckboxGrid(category, items);
            grid.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            host.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            host.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            host.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            host.Controls.Add(headerLabel, 0, g * 3);
            host.Controls.Add(ruleLine, 0, g * 3 + 1);
            host.Controls.Add(grid, 0, g * 3 + 2);
        }

        return host;
    }

    private TableLayoutPanel BuildCheckboxGrid(string category, List<SettingDefinition> items)
    {
        var rows = Math.Max((int)Math.Ceiling(items.Count / (double)GridColumns), 1);

        var grid = new TableLayoutPanel
        {
            ColumnCount = GridColumns,
            RowCount = rows,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            BorderStyle = BorderStyle.FixedSingle,
            Padding = new Padding(10),
            Margin = new Padding(0, 0, 0, 4),
        };

        for (var c = 0; c < GridColumns; c++)
        {
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / GridColumns));
        }

        for (var r = 0; r < rows; r++)
        {
            grid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        }

        for (var i = 0; i < items.Count; i++)
        {
            var def = items[i];
            var checkBox = new CheckBox
            {
                Text = def.Label,
                Checked = _values[def.Key],
                AutoSize = true,
                Margin = new Padding(3, 6, 3, 6),
            };
            checkBox.CheckedChanged += (_, _) =>
            {
                _values[def.Key] = checkBox.Checked;
                UpdateGroupHeader(category);
            };
            checkBox.MouseEnter += (_, _) =>
                SetStatus(string.IsNullOrEmpty(def.Description) ? DefaultStatusText : def.Description);
            checkBox.MouseLeave += (_, _) => SetStatus(DefaultStatusText);

            _checkboxes[def.Key] = checkBox;
            grid.Controls.Add(checkBox, i % GridColumns, i / GridColumns);
        }

        return grid;
    }

    private Control BuildBottomBar()
    {
        var bar = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 3,
            RowCount = 1,
            AutoSize = true,
            Margin = new Padding(0, 8, 0, 0),
        };
        bar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
        bar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32));
        bar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
        bar.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        var aboutButton = new Button
        {
            Text = "About",
            AutoSize = true,
            Padding = new Padding(10, 2, 10, 2),
            Anchor = AnchorStyles.Left,
        };
        aboutButton.Click += (_, _) => MessageBox.Show(
            this,
            "Blossom Tales 2 Randomizer\n\nA small generic editor for boolean application settings.\n" +
            "Edit the catalog in Models/SettingsCatalog.cs to customize the list.",
            "About",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        var resetButton = new Button
        {
            Text = "Reset All Settings to Default",
            AutoSize = true,
            Padding = new Padding(10, 2, 10, 2),
            Anchor = AnchorStyles.None,
        };
        resetButton.Click += (_, _) => ResetToDefaults();

        var saveButton = new Button
        {
            Text = "Save",
            AutoSize = true,
            Padding = new Padding(10, 2, 10, 2),
            Anchor = AnchorStyles.Right,
        };
        saveButton.Click += (_, _) => SaveSettings();

        bar.Controls.Add(aboutButton, 0, 0);
        bar.Controls.Add(resetButton, 1, 0);
        bar.Controls.Add(saveButton, 2, 0);

        return bar;
    }

    private string FormatHeader(string category, List<SettingDefinition> items) =>
        $"{category}  (Selected: {items.Count(i => _values[i.Key])} of {items.Count})";

    private void UpdateGroupHeader(string category)
    {
        var (header, items) = _groupHeaders[category];
        header.Text = FormatHeader(category, items);
    }

    private void SetStatus(string text) => _statusLabel.Text = text;

    private void SaveSettings()
    {
        try
        {
            SettingsStore.Save(_values);
            SetStatus($"Saved to {SettingsStore.GetSettingsPath()}");
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            MessageBox.Show(this, $"Could not save settings:\n{ex.Message}", "Save failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ResetToDefaults()
    {
        var confirm = MessageBox.Show(
            this,
            "Reset all settings to their default values?",
            "Reset All Settings to Default",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes)
        {
            return;
        }

        foreach (var def in _catalog)
        {
            _values[def.Key] = def.DefaultValue;
            _checkboxes[def.Key].Checked = def.DefaultValue;
        }

        foreach (var category in _groupHeaders.Keys)
        {
            UpdateGroupHeader(category);
        }

        SetStatus(DefaultStatusText);
    }
}
