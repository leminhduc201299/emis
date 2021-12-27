import request from "../../utils/request";

const state = {
    searchTags: []
}
  
const mutations = {
    SET_SEARCH_TAGS: (state, searchTags) => {
        state.searchTags = searchTags
    },
}

const actions = {
    // Get search tags
    getSearchTags({ commit }, keySearch) {
        const { gradeId, subjectId } = keySearch;

        request(
            {
                url: `/SearchTags/Filter?gradeId=${gradeId}&subjectId=${subjectId}`,
                method: "GET",
            }, false
          )
            .then((res) => {
                let searchTagOptions = res.map((item) => item.SearchTagName)
                commit('SET_SEARCH_TAGS', searchTagOptions)
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
