import Vue from 'vue'
import Vuex from 'vuex'
import exercise from './modules/exercise'
import grade from './modules/grade'
import subject from './modules/subject'
import topic from './modules/topic'
import searchTag from './modules/searchTag'
import getters from './getter'

Vue.use(Vuex)

const store = new Vuex.Store({
    modules: {
        exercise,
        grade,
        subject,
        topic,
        searchTag,
    },
    getters
})

  export default store;