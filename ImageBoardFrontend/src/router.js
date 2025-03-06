import { createRouter, createWebHistory } from "vue-router";
import PostList from "@/components/PostList.vue";
import PostView from "@/views/PostView.vue";

const routes = [
  { path: "/", component: PostList },
  { path: "/post/:id", component: PostView, props: true },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;