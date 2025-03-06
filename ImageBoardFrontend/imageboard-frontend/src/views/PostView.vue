<script setup>
import { ref, onMounted } from "vue";
import axios from "axios";
import { useRoute } from "vue-router";

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

const route = useRoute();
const post = ref(null);

const fetchPost = async () => {
  try {
    const response = await axios.get(`${API_BASE_URL}/api/post/by-id/${route.params.id}`);
    post.value = response.data;
  } catch (error) {
    console.error("Error fetching post:", error);
  }
};

onMounted(fetchPost);
</script>

<template>
  <div v-if="post" class="post-container">
    <h2>{{ post.title }}</h2>
    <p>{{ post.content }}</p>
    <small>Category: {{ post.category || "Uncategorized" }} | Author: {{ post.author || "Anonymous" }}</small>
    <a v-if="post.imageUrl" :href="API_BASE_URL + post.imageUrl" target="_blank">
      <img :src="API_BASE_URL + post.imageUrl" alt="Uploaded image" />
    </a>
    <br />
    <router-link to="/">⬅ Back to Posts</router-link>
  </div>
  <p v-else>Loading post...</p>
</template>

<style scoped>
.post-container {
  max-width: 600px;
  margin: 40px auto;
  padding: 20px;
  background: white;
  border-radius: 8px;
  box-shadow: 0px 4px 6px rgba(0, 0, 0, 0.1);
}

img {
  max-width: 100%;
  border-radius: 5px;
}
</style>