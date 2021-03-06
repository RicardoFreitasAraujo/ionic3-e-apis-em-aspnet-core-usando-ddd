﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using YouLearn.Domain.Entities;
using YouLearn.Shared;
using YouLearn.Infra.Persistence.EF.Map;
using prmToolkit.NotificationPattern;
using YouLearn.Domain.ValueObjects;

namespace YouLearn.Infra.Persistence.EF
{
    public class YouLearnContext: DbContext
    {
        public DbSet<Canal> Canais { get; set; }
        public DbSet<PlayList> PlayLists { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        //public DbSet<Favorito> Favoritos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Settings.ConnectionString);
            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Ignorar classes
            modelBuilder.Ignore<Notification>();
            modelBuilder.Ignore<Nome>();
            modelBuilder.Ignore<Email>();

            //Aplicar configurações
            modelBuilder.ApplyConfiguration(new MapCanal());
            modelBuilder.ApplyConfiguration(new MapPlayList());
            modelBuilder.ApplyConfiguration(new MapVideo());
            modelBuilder.ApplyConfiguration(new MapUsuario());
            //modelBuilder.ApplyConfiguration(new MapFavorito());

            base.OnModelCreating(modelBuilder);
        }
    }
}
