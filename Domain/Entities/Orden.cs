﻿namespace Domain.Entities
{
    public class Orden
    {
        public Guid OrdenId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

        public Guid CarritoId { get; set; }
        public Carrito Carrito { get; set; }
    }
}
