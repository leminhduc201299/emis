import Vue from 'vue'
import VueRouter from 'vue-router'

import ExerciseList from '../views/exercise/ExerciseList.vue'
import QuestionList from '../views/question/QuestionList.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'ExerciseList',
    component: ExerciseList
  },
  {
    path: '/question',
    name: 'QuestionList',
    component: QuestionList
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
