import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      redirect: '/notes'
    },
    {
      path: '/notes',
      name: 'notes',
      component: () => import('../views/NotesView.vue'),
      children: [
        {
          path: '/notes',
          name: 'no-note',
          component: () => import('../views/Notes/NoNoteView.vue'),
        },
        {
          path: '/notes/:id',
          name: 'note-details',
          component: () => import('../views/Notes/NoteView.vue'),
        }
      ]
    },
    {
      path: '/files',
      name: 'files',
      component: () => import('../views/FilesView.vue')
    },
    {
      path: '/about',
      name: 'about',
      component: () => import('../views/AboutView.vue')
    }
  ]
})

export default router
