using AutoMapper;
using SchoolPortal.Common.DTOs;
using SchoolPortal.Common.Models.dboSchema;
using StateService.API.Repository;

namespace StateService.API.Service
{
    public class StateService : IStateService
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;

        public StateService(IStateRepository StateRepository, IMapper mapper)
        {
            _stateRepository = StateRepository;
            _mapper = mapper;
        }

        public async Task<ViewStateDto?> AddAsync(CreateStateDto dto)
        {
            var State = _mapper.Map<StateMaster>(dto);
            State.CreatedDate = DateTime.UtcNow;
            State.ModifiedDate = null;
            State.ModifiedBy = null;
            await _stateRepository.CreateAsync(State);
            return _mapper.Map<ViewStateDto?>(State);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await DeleteAsync(id, new Guid("368BD1DF-42D6-4F05-9CA0-8DF22191B917")); // Default system user
        }

        public async Task<bool> DeleteAsync(Guid id, Guid modifiedBy)
        {
            var state = await _stateRepository.GetByIdAsync(id);
            if (state == null) return false;

            state.IsDeleted = true;
            state.IsActive = false;
            state.ModifiedDate = DateTime.UtcNow;
            state.ModifiedBy = modifiedBy;
            state.Status = "Deleted";
            state.StatusMessage = "State Deleted Successfully";

            return await _stateRepository.UpdateAsync(id, state);
        }

        public async Task<IEnumerable<ViewStateDto?>> GetAllAsync()
        {
            var states = await _stateRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ViewStateDto?>>(states);
        }

        public async Task<UpdateStateDto?> GetByIdAsync(Guid id)
        {
            var State = await _stateRepository.GetByIdAsync(id);
            return State == null ? null : _mapper.Map<UpdateStateDto?>(State);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateStateDto dto)
        {
            var State = await _stateRepository.GetByIdAsync(id);
            if (State == null) return false;

            _mapper.Map(dto, State);
            State.ModifiedDate = DateTime.UtcNow;
            State.ModifiedBy = dto.ModifiedBy;
            return await _stateRepository.UpdateAsync(id, State);
        }
    }
}
