using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GestionTorneosDeportivos.Modelos;

    public class GestionTorneosAPIContext : DbContext
    {
        public GestionTorneosAPIContext (DbContextOptions<GestionTorneosAPIContext> options)
            : base(options)
        {
        }

        public DbSet<GestionTorneosDeportivos.Modelos.Equipo> Equipos { get; set; } = default!;

        public DbSet<GestionTorneosDeportivos.Modelos.EventoPartido> EventosPartidos { get; set; } = default!;

        public DbSet<GestionTorneosDeportivos.Modelos.Jugador> Jugadores { get; set; } = default!;

        public DbSet<GestionTorneosDeportivos.Modelos.Partido> Partidos { get; set; } = default!;

        public DbSet<GestionTorneosDeportivos.Modelos.Torneo> Torneos { get; set; } = default!;

public DbSet<GestionTorneosDeportivos.Modelos.TorneoEquipo> TorneosEquipos { get; set; } = default!;
    }
