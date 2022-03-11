﻿using cms_bd.Models;
using Microsoft.EntityFrameworkCore;

namespace cms_bd.Data
{
    public static class DbInitializer
    {
        public static bool InitializeDefaultUser(DataContext context)
        {
            if (context.Users.Any())
            {
                return false;
            }

            var users = new User[]
            {
                new() { UserName = "admin", Email = "admin@admin.com" },
            };
            context.Users.AddRange(users);
            context.SaveChanges();

            return true;
        }

        public static void Initialize(DataContext context)
        {
            // var images = new ImageMetadata[]
            // {
            //     new() { FileName = "mainpage.jpg", CreatedBy = 1 },
            //     new() { FileName = "coupon.jpg", CreatedBy = 1 },
            //     new() { FileName = "post.jpg", CreatedBy = 1 },
            // };
            // context.ImageMetadata.AddRange(images);
            // context.SaveChanges();

            var config = new Config[]
            {
                new() { Key = "BackgroundImage", Value = "mainpage.jpg", UpdatedBy = 1 },
                new() { Key = "BackgroundColor", Value = "#ffffff", UpdatedBy = 1 },
                new() { Key = "ContentTitle", Value = "Example content title", UpdatedBy = 1 },
            };
            context.Config.AddRange(config);
            context.SaveChanges();

            var coupons = new Coupon[]
            {
                new() { Name = "Coupon2", ImageID = 2, Description = "example description 2", Code = "2222", Order = 2, ValidFrom = DateTime.Now, ValidTo = DateTime.Now.AddDays(10), IsVisible = 1, UpdatedBy = 1 },
                new() { Name = "Coupon1", ImageID = 2, Description = "example description 1", Code = "1111", Order = 1, ValidFrom = DateTime.Now, ValidTo = DateTime.Now.AddDays(10), IsVisible = 1, UpdatedBy = 1 },
            };
            context.Coupons.AddRange(coupons);
            context.SaveChanges();

            var tags = new Tag[]
            {
                new() { Name = "Tag1", IsArchived = 0, UpdatedBy = 1},
                new() { Name = "Tag2", IsArchived = 0, UpdatedBy = 1},
                new() { Name = "Tag3", IsArchived = 0, UpdatedBy = 1},
            };
            context.Tags.AddRange(tags);
            context.SaveChanges();

            var tagCouponPivots = new TagCouponPivot[]
            {
                new() { CouponID = 1, TagID = 1 },
                new() { CouponID = 1, TagID = 3 },
                new() { CouponID = 2, TagID = 2 },
            };
            context.TagCouponPivot.AddRange(tagCouponPivots);
            context.SaveChanges();

            var posts = new Post[]
            {
                new() { Title = "Post4", Icon = "home", ImageID = 3, Content = "example content 4", IsVisible = 1, IsInMenu = 1, Order = 4, UpdatedBy = 1 },
                new() { Title = "Post3", Icon = "home", ImageID = 3, Content = "example content 3", IsVisible = 1, IsInMenu = 0, Order = 3, UpdatedBy = 1 },
                new() { Title = "Post2", Icon = "home", ImageID = 3, Content = "example content 2", IsVisible = 1, IsInMenu = 1, Order = 2, UpdatedBy = 1 },
                new() { Title = "Post1", Icon = "home", ImageID = 3, Content = "example content 1", IsVisible = 1, IsInMenu = 0, Order = 1, UpdatedBy = 1 },
            };
            context.Posts.AddRange(posts);
            context.SaveChanges();

            var usedCoupons = new UsedCoupon[]
            {
                new() { CouponID = 1, UserID = 1 },
                new() { CouponID = 2, UserID = 1 },
            };
            context.UsedCoupons.AddRange(usedCoupons);
            context.SaveChanges();
        }

        public static void UpdateImageDirectoryMetadata(DataContext context)
        {
            var folderName = Path.Combine("resources", "images");
            var pathToRead = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (!Directory.Exists(pathToRead))
            {
                Directory.CreateDirectory(pathToRead);
                return;
            }

            var files = Directory.EnumerateFiles(pathToRead)
                .Where(IsAPhotoFile)
                .Select(Path.GetFileName);
            var imageMetadata = context.ImageMetadata.ToListAsync().Result;

            var toAdd = new ImageMetadata[] { };
            toAdd = (
                from file in files
                let isDuplicate = imageMetadata.Any(f => f.FileName == file)
                where !isDuplicate
                select file
                )
                .Aggregate(toAdd, (current, file) =>
                    current.Append(new ImageMetadata { FileName = file, CreatedBy = 1 }).ToArray());

            context.ImageMetadata.AddRange(toAdd);
            context.SaveChanges();
        }

        public static bool IsAPhotoFile(string fileName)
        {
            return fileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
                   || fileName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)
                   || fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase);
        }
    }
}