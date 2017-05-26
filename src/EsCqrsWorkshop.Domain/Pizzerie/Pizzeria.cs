﻿using System;
using System.Collections.Generic;
using System.Linq;
using EsCqrsWorkshop.Domain.ValueObjects;
using Radical.CQRS;
using Topics.Radical.Linq;

namespace EsCqrsWorkshop.Domain.Pizzerie
{
    public class Pizzeria : Aggregate<Pizzeria.PizzeriaState>
    {
        public class PizzeriaState : AggregateState
        {
            public string Name { get; set; }
            public ISet<Order> Orders { get; set; } = new HashSet<Order>();
        }


        private Pizzeria(Pizzeria.PizzeriaState state)
            : base(state)
        {
        }

        public class Factory
        {
            public Pizzeria CreateNew(string nome)
            {
                var state = new PizzeriaState()
                {
                    Name = nome,
                    Orders = new HashSet<Order>()
                };
                var aggregate = new Pizzeria(state);
                aggregate.SetupCompleted();
                return aggregate;
            }
        }

        private void SetupCompleted()
        {
            this.RaiseEvent<IPizzeriaCreated>(e =>
            {
                e.Name = this.Data.Name;
            });
        }

    }
}
