import request from "../../utils/request";

const state = {
    subjects: []
}
  
const mutations = {
    SET_SUBJECTS: (state, subjects) => {
        state.subjects = subjects
    },
}

const actions = {
    // Get subjects
    getSubjects({ commit }) {
        request(
            {
                url: '/Subjects',
                method: "GET",
            }, false
          )
            .then((res) => {
                let subjectOptions = res.map((item) => ( {text: item.SubjectName, value: item.SubjectId} ))
                commit('SET_SUBJECTS', subjectOptions)
            })
            .catch((error) => {
              console.log(error);
            });
    },
}

export default {
    namespaced: true,
    state,
    mutations,
    actions
}
