﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace bcquery_mvc
{
    public class TranslationEntities : DbContext
    {

        public TranslationEntities()
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public DbSet<Language> Languages { get; set; }
        public DbSet<TranslationItem> TranslationItems { get; set; }
        public DbSet<TranslationText> TranslationTexts { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}