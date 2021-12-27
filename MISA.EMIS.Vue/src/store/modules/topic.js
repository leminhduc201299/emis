import request from "../../utils/request";

const state = {
    topics: []
}
  
const mutations = {
    SET_TOPICS: (state, topics) => {
        state.topics = topics
    },
}

const actions = {
    // Get topics
    getTopics({ commit }, keySearch) {
        const { gradeId, subjectId } = keySearch;

        request(
            {
                url: `/Topics/Filter?gradeId=${gradeId}&subjectId=${subjectId}`,
                method: "GET",
            }, false
          )
            .then((res) => {
                let topicOptions = res.map((item) => ( {text: item.TopicName, value: item.TopicId} ))
                commit('SET_TOPICS', topicOptions)
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
