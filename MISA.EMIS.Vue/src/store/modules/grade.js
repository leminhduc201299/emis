import request from "../../utils/request";

const state = {
    grades: []
}
  
const mutations = {
    SET_GRADES: (state, grades) => {
        state.grades = grades
    },
}

const actions = {
    // Get grades
    getGrades({ commit }) {
        request(
            {
                url: '/Grades',
                method: "GET",
            }, false
          )
            .then((res) => {
                let gradeOptions = res.map((item) => ( {text: item.GradeName, value: item.GradeId} ))
                commit('SET_GRADES', gradeOptions)
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
