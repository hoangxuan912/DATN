using System.Numerics;
using asd123.DTO;
using asd123.Model;
using Microsoft.EntityFrameworkCore;

namespace asd123.Services;

public interface ISinhVien
{
    Task<IEnumerable<SinhVien>> GetAllSinhVienAsync();
    Task<SinhVien> GetSinhVienByIdAsync(Guid id);
    Task<SinhVien> CreateSinhVienAsync(SinhVien sinhVien);
    Task UpdateSinhVienAsync(SinhVien sinhVien);
    Task DeleteSinhVienAsync(Guid id);
    Task<IEnumerable<SinhVien>> GetSinhVienByLopIdAsync(Guid lopId, PaginationDTO pagination);
    Task<IEnumerable<SinhVien>> SearchSinhVienAsync(string searchText, PaginationDTO pagination);
}

public class SinhVienService : ISinhVien
{
    private readonly ApplicationDbContext _context;

    public SinhVienService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SinhVien>> GetAllSinhVienAsync()
    {
        return await _context.SinhViens.Include(sv => sv.Lop).ToListAsync();
    }

    public async Task<SinhVien> GetSinhVienByIdAsync(Guid id)
    {
        return await _context.SinhViens.Include(sv => sv.Lop)
            .SingleOrDefaultAsync(sv => sv.Id == id);
    }

    public async Task<SinhVien> CreateSinhVienAsync(SinhVien sinhVien)
    {
        if (sinhVien == null) throw new ArgumentNullException(nameof(sinhVien));

        await _context.SinhViens.AddAsync(sinhVien);
        await _context.SaveChangesAsync();
        return sinhVien; // Trả về đối tượng SinhVien vừa được thêm vào cơ sở dữ liệu.
    }

    public async Task UpdateSinhVienAsync(SinhVien sinhVien)
    {
        if (sinhVien == null) throw new ArgumentNullException(nameof(sinhVien));

        _context.SinhViens.Update(sinhVien);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSinhVienAsync(Guid id)
    {
        var sinhVien = await _context.SinhViens.FindAsync(id);
        if (sinhVien != null)
        {
            _context.SinhViens.Remove(sinhVien);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<SinhVien>> GetSinhVienByLopIdAsync(Guid lopId, PaginationDTO pagination)
    {
        return await _context.SinhViens
            .Where(sv => sv.MaLop == lopId)
            .Include(sv => sv.Lop)
            .OrderBy(sv => sv.Id) // Thêm bước sắp xếp theo Id ở đây
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<SinhVien>> SearchSinhVienAsync(string searchText, PaginationDTO pagination)
    {
        return await _context.SinhViens
            .Where(sv => sv.HoTen.Contains(searchText) ||
                         sv.Email.Contains(searchText) ||
                         sv.SoDienThoai.Contains(searchText))
            .Include(sv => sv.Lop)
            .OrderBy(sv => sv.Id) // Thêm bước sắp xếp theo Id ở đây
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }
}