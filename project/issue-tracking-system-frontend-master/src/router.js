import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'
import Project from './views/Project.vue'
import Login from './components/Login.vue'
import Register from './components/Register.vue'
import LoginRegisterDialog from './views/LoginRegisterDialog.vue'
import IssueIndex from './views/IssueIndex.vue'
import IssuePage from './views/IssuePage.vue'
import AccountManagement from './views/AccountManagement.vue'
import Profile from './views/Profile.vue'
import Report from './views/Report.vue'

Vue.use(Router)

export default new Router({
  routes: [{
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import( /* webpackChunkName: "about" */ './views/About.vue')
    },
    {
      path: '/project',
      name: 'project',
      component: Project
    },
    {
      path: '/login',
      name: 'login',
      component: LoginRegisterDialog
    },
    {
      path: '/register',
      name: 'Register',
      component: Register
    }, 
    {
      path: '/accountmanagement',
      name: 'AccountManagement',
      component: AccountManagement
    },
    {
      path: '/issue',
      name: 'IssueIndex',
      component: IssueIndex
    },
    {
      path: '/issue/:id',
      name: 'IssuePage',
      component: IssuePage
    },
    {
      path: '/profile',
      name: 'Profile',
      component: Profile
    },
    {
      path: '/report',
      name: 'Report',
      component: Report
    }
  ]
})
