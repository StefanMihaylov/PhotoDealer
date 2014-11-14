namespace PhotoDealer.Web.Infrastructure.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PhotoDealer.Data.Models;

    public class FilterResults
    {

        public IQueryable<T> Pagenation<T>(IQueryable<T> query, int page, int pageSize)
        {
            if (page <= 0)
            {
                page = 1;
            }

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IQueryable<Picture> FilterPictures(IQueryable<Picture> query, SearchViewModel search)
        {
            if (query == null)
            {
                return null;
            }

            query = OrderByPictures(query, search);

            if (!string.IsNullOrWhiteSpace(search.Title))
            {
                query = query.Where(p => p.Title.Contains(search.Title));
            }

            if (!string.IsNullOrWhiteSpace(search.Author))
            {
                query = query.Where(p => p.Author.UserName.Contains(search.Author));
            }

            if (search.CategoryGroup > 0)
            {
                query = query.Where(p => p.CategoryGroupId == search.CategoryGroup);
            }

            if (search.Category > 0)
            {
                query = query.Where(p => p.CategoryId == search.Category);
            }

            if (!string.IsNullOrWhiteSpace(search.Tag))
            {
                query = query.Where(p => p.Tags.Any(t => t.Content.Contains(search.Tag)));
            }

            if (search.PriceFrom > 0)
            {
                query = query.Where(p => p.Price >= search.PriceFrom);
            }

            if (search.PriceTo > 0)
            {
                query = query.Where(p => p.Price <= search.PriceTo);
            }

            return query;
        }

        private IQueryable<Picture> OrderByPictures(IQueryable<Picture> query, SearchViewModel search)
        {
            if (query == null)
            {
                return null;
            }

            if (search.OrderType == OrderTypeEnum.Ascending)
            {
                switch (search.OrderBy)
                {
                    case OrderByEnum.None:
                        query = query.OrderBy(p => p.PictureId);
                        break;
                    case OrderByEnum.Title:
                        query = query.OrderBy(p => p.Title);
                        break;
                    case OrderByEnum.Price:
                        query = query.OrderBy(p => p.Price);
                        break;
                    case OrderByEnum.Author:
                        query = query.OrderBy(p => p.Author.UserName);
                        break;
                    case OrderByEnum.Date:
                        query = query.OrderBy(p => p.CreatedOn);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("Unknown value of OrderByEnum enumeration");
                }
            }
            else
            {
                switch (search.OrderBy)
                {
                    case OrderByEnum.None:
                        query = query.OrderByDescending(p => p.PictureId);
                        break;
                    case OrderByEnum.Title:
                        query = query.OrderByDescending(p => p.Title);
                        break;
                    case OrderByEnum.Price:
                        query = query.OrderByDescending(p => p.Price);
                        break;
                    case OrderByEnum.Author:
                        query = query.OrderByDescending(p => p.Author.UserName);
                        break;
                    case OrderByEnum.Date:
                        query = query.OrderByDescending(p => p.CreatedOn);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("Unknown value of OrderByEnum enumeration");
                }
            }

            return query;
        }

    }
}
