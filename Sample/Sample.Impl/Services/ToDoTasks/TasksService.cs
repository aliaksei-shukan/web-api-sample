﻿using Microsoft.EntityFrameworkCore;
using Sample.DAL;
using Sample.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Impl.Services.ToDoTasks
{
    public class TasksService : ITasksService
    {
        private readonly DataContext _context;

        public TasksService(DataContext context)
        {
            _context = context;
        }

        public async void CreateAsync(ToDoItem item)
        {
            if (item == null)
            {
                throw new System.Exception("You are trying to create an empty entity!");
            }

            _context.ToDoItems.Add(item);

            await _context.SaveChangesAsync();
        }

        public async void DeleteAsync(int? id)
        {
            if (!id.HasValue)
            {
                throw new System.Exception("Requested id is null!");
            }

            var item = await _context.ToDoItems.FindAsync(id);

            if (item == null)
            {
                throw new System.Exception("You are trying to delete not existing entity!");
            }

            _context.ToDoItems.Remove(item);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ToDoItem>> GetAllAsync()
        {
            return await _context.ToDoItems.ToListAsync();
        }

        public async Task<ToDoItem> GetAsync(int? id)
        {
            if (!id.HasValue)
            {
                throw new System.Exception("Requested id is null!");
            }

            var item = await _context.ToDoItems.FindAsync(id);

            if (item == null)
            {
                throw new System.Exception("You are trying to get not existing entity!");
            }

            return item;
        }

        public async void UpdateAsync(ToDoItem item)
        {
            if (item == null)
            {
                throw new System.Exception("Requested id is null!");
            }

            var updatingItem = await _context.ToDoItems.FindAsync(item.Id);

            if (updatingItem == null)
            {
                throw new System.Exception("You are trying to update not existing entity!");
            }

            _context.ToDoItems.Update(item);

            await _context.SaveChangesAsync();
        }
    }
}