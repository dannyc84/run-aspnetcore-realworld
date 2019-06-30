﻿using AspnetRun.Core.Entities;
using AspnetRun.Core.Repositories;
using AspnetRun.Core.Specifications;
using AspnetRun.Infrastructure.Data;
using AspnetRun.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetRun.Infrastructure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AspnetRunContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Product>> GetProductListAsync()
        {
            var spec = new ProductWithCategorySpecification();
            return await GetAsync(spec);

            // second way
            // return await GetAllAsync();
        }

        public Task<Product> GetProductBySlug(string slug)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetProductByNameAsync(string productName)
        {
            var spec = new ProductWithCategorySpecification(productName);
            return await GetAsync(spec);

            // second way
            // return await GetAsync(x => x.ProductName.ToLower().Contains(productName.ToLower()));

            // third way
            //return await _dbContext.Products
            //    .Where(x => x.ProductName.Contains(productName))
            //    .ToListAsync();
        }

        public async Task<Product> GetProductByIdWithCategoryAsync(int productId)
        {
            var spec = new ProductWithCategorySpecification(productId);
            return (await GetAsync(spec)).FirstOrDefault();
        }

        public async Task<IEnumerable<Product>> GetProductByCategoryAsync(int categoryId)
        {
            return await _dbContext.Products
                .Where(x => x.CategoryId==categoryId)
                .ToListAsync();
        }
        
    }
}
