using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeLewis.Data;
using RecipeLewis.DataExtensions;
using RecipeLewis.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeLewis.Services
{
    public class TagService
    {
        private readonly ILogger _logger;

        public TagService(ApplicationDbContext context, ILogger<Tag> logger)
        {
            _context = context;
            _logger = logger;
        }

        public ApplicationDbContext _context { get; set; }

        public async Task<ServiceResult<TagModel>> GetAllTags()
        {
            try
            {
                _logger.LogInformation("Getting tags");
                var list = await _context.Tags.Where(x => x.DeletedDateTime == null).ToListAsync();
                _logger.LogInformation("Tags returned: " + list.Count);
                return new ServiceResult<TagModel>(true) { DataList = list.Select(x => x.ToModel()).ToList() };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tags failed to get all");
                return new ServiceResult<TagModel>(false, ex.Message);
            }
        }

        public async Task<ServiceResult<TagModel>> GetTag(int tagId)
        {
            try
            {
                var data = await _context.Tags.FirstOrDefaultAsync(x => x.TagID == tagId && x.DeletedDateTime == null);
                return new ServiceResult<TagModel>(true) { Data = data.ToModel() };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Recipes failed to get all");
                return new ServiceResult<TagModel>(false, ex.Message);
            }
        }
    }
}