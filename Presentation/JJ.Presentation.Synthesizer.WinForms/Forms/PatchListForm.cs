using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Configuration;
using JJ.Framework.Data;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Presentation.Svg.EventArg;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.Presenters;
using JJ.Presentation.Synthesizer.Svg;
using JJ.Presentation.Synthesizer.Svg.EventArg;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.WinForms.Configuration;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.Svg.Helpers;

namespace JJ.Presentation.Synthesizer.WinForms
{
    public partial class PatchListForm : Form
    {
        private IContext _context;

        public PatchListForm()
        {
            InitializeComponent();

            _context = PersistenceHelper.CreateContext();

            patchListUserControl1.Context = _context;
            patchListUserControl1.Show(1);
        }

        private void PatchListForm_Load(object sender, EventArgs e)
        {
        }

        private void patchListUserControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
