using Exercises.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exercises.Models.Repositories
{
    public class StateRepository
    {
        private static List<State> _states;
        private static List<State> _allStates;
        private static List<State> _availableStates;

        static StateRepository()
        {
            // sample data
            _states = new List<State>
            {
                new State { StateAbbreviation="KY", StateName="Kentucky" },
                new State { StateAbbreviation="MN", StateName="Minnesota" },
                new State { StateAbbreviation="OH", StateName="Ohio" },
            };

            _allStates = new List<State>
            {
            new State { StateAbbreviation="AL", StateName="Alabama"},
            new State { StateAbbreviation="AK", StateName="Alaska"},
            new State { StateAbbreviation="AZ", StateName="Arizona"},
            new State { StateAbbreviation="AR", StateName="Arkansas"},
            new State { StateAbbreviation="CA", StateName="California"},
            new State { StateAbbreviation="CO", StateName="Colorado"},
            new State { StateAbbreviation="CT", StateName="Connecticut"},
            new State { StateAbbreviation="DE", StateName="Delaware"},
            new State { StateAbbreviation="DC", StateName="District Of Columbia"},
            new State { StateAbbreviation="FL", StateName="Florida"},
            new State { StateAbbreviation="GA", StateName="Georgia"},
            new State { StateAbbreviation="HI", StateName="Hawaii"},
            new State { StateAbbreviation="ID", StateName="Idaho"},
            new State { StateAbbreviation="IL", StateName="Illinois"},
            new State { StateAbbreviation="IN", StateName="Indiana"},
            new State { StateAbbreviation="IA", StateName="Iowa"},
            new State { StateAbbreviation="KS", StateName="Kansas"},
            new State { StateAbbreviation="KY", StateName="Kentucky"},
            new State { StateAbbreviation="LA", StateName="Louisiana"},
            new State { StateAbbreviation="ME", StateName="Maine"},
            new State { StateAbbreviation="MD", StateName="Maryland"},
            new State { StateAbbreviation="MA", StateName="Massachusetts"},
            new State { StateAbbreviation="MI", StateName="Michigan"},
            new State { StateAbbreviation="MN", StateName="Minnesota"},
            new State { StateAbbreviation="MS", StateName="Mississippi"},
            new State { StateAbbreviation="MO", StateName="Missouri"},
            new State { StateAbbreviation="MT", StateName="Montana"},
            new State { StateAbbreviation="NE", StateName="Nebraska"},
            new State { StateAbbreviation="NV", StateName="Nevada"},
            new State { StateAbbreviation="NH", StateName="New Hampshire"},
            new State { StateAbbreviation="NJ", StateName="New Jersey"},
            new State { StateAbbreviation="NM", StateName="New Mexico"},
            new State { StateAbbreviation="NY", StateName="New York"},
            new State { StateAbbreviation="NC", StateName="North Carolina"},
            new State { StateAbbreviation="ND", StateName="North Dakota"},
            new State { StateAbbreviation="OH", StateName="Ohio"},
            new State { StateAbbreviation="OK", StateName="Oklahoma"},
            new State { StateAbbreviation="OR", StateName="Oregon"},
            new State { StateAbbreviation="PA", StateName="Pennsylvania"},
            new State { StateAbbreviation="RI", StateName="Rhode Island"},
            new State { StateAbbreviation="SC", StateName="South Carolina"},
            new State { StateAbbreviation="SD", StateName="South Dakota"},
            new State { StateAbbreviation="TN", StateName="Tennessee"},
            new State { StateAbbreviation="TX", StateName="Texas"},
            new State { StateAbbreviation="UT", StateName="Utah"},
            new State { StateAbbreviation="VT", StateName="Vermont"},
            new State { StateAbbreviation="VA", StateName="Virginia"},
            new State { StateAbbreviation="WA", StateName="Washington"},
            new State { StateAbbreviation="WV", StateName="West Virginia"},
            new State { StateAbbreviation="WI", StateName="Wisconsin"},
            new State { StateAbbreviation="WY", StateName="Wyoming"},
            };
        }

        public static IEnumerable<State> GetAll()
        {
            return _states;
        }

        public static IEnumerable<State> AllStates()
        {
            return _allStates;
        }

        public static IEnumerable<State> AvailableStates()
        {
            _availableStates = new List<State>();
            foreach (var item in _allStates)
            {
                if (!_states.Any(s=>s.StateAbbreviation == item.StateAbbreviation))
                {
                    _availableStates.Add(item);
                }
            }
            return _availableStates;
        }

        public static State Get(string stateAbbreviation)
        {
            return _states.FirstOrDefault(c => c.StateAbbreviation == stateAbbreviation);
        }

        public static void Add(State state)
        {
            _states.Add(state);
        }

        public static void Edit(State state)
        {
            var selectedState = _states.FirstOrDefault(c => c.StateAbbreviation == state.StateAbbreviation);

            selectedState.StateName = state.StateName;
        }

        public static void Delete(string stateAbbreviation)
        {
            _states.RemoveAll(c => c.StateAbbreviation == stateAbbreviation);
        }
    }
}