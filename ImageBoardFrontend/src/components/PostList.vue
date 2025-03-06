<script setup>
import { ref, onMounted } from "vue";
import axios from "axios";

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

const posts = ref([]);
const newPost = ref({
  title: "",
  content: "",
  author: "",
  category: "",
  file: null,
});

const showForm = ref(false);

const fetchPosts = async () => {
  try {
    const response = await axios.get(`${API_BASE_URL}/api/post/all`);
    posts.value = response.data.sort((a, b) => new Date(b.createdAt) - new Date(a.createdAt));
  } catch (error) {
    console.error("Error fetching posts:", error);
  }
}

const handleFileChange = (event) => {
  newPost.value.file = event.target.files[0];
};

const submitPost = async () => {
  const formData = new FormData();
  formData.append("title", newPost.value.title);
  formData.append("content", newPost.value.content);
  formData.append("author", newPost.value.author);
  formData.append("category", newPost.value.category);
  if (newPost.value.file) {
    formData.append("file", newPost.value.file);
  }

  try {
    await axios.post(`${API_BASE_URL}/api/Post`, formData, {
      headers: { "Content-Type": "multipart/form-data" },
    });
    newPost.value = { title: "", content: "", author: "", category: "", file: null };
    fetchPosts();
    showForm.value = false;
  } catch (error) {
    console.error("Error submitting post:", error);
  }
};

onMounted(fetchPosts);
</script>

<template>
  <div class="container">
    <button @click="showForm = !showForm" class="toggle-btn">
      <!-- {{ showForm ? "Hide Form" : "Create a Post" }} -->
      New Post
    </button>

    <!-- New Post Form -->
    <div v-if="showForm" class="form-container">
      <h2>Create a Post</h2>
      <form @submit.prevent="submitPost">

        <label>Title</label>
        <input v-model="newPost.title" type="text" required />

        <label>Author (Optional)</label>
        <input v-model="newPost.author" type="text" placeholder="Anonymous" />


        <label>Category (Optional)</label>
        <input v-model="newPost.category" type="text" placeholder="Uncategorised" />

        <label>Content (Optional)</label>
        <textarea v-model="newPost.content" rows="3" placeholder="Write something..."></textarea>

        <label>Upload Image</label>
        <input type="file" @change="handleFileChange" accept="image/*" />

        <button type="submit">Submit Post</button>
      </form>
    </div>

    <!-- Posts Feed -->
    <div v-if="posts.length" class="posts-container">
      <div v-for="post in posts" :key="post.id" class="post">
        <router-link :to="'/post/' + post.id">
          <h3>{{ post.title }}</h3>
        </router-link>
        <p>{{ post.content }}</p>
        <a v-if="post.imageUrl" :href="`${API_BASE_URL + post.imageUrl}`" target="_blank">
          <img :src="`${API_BASE_URL + post.imageUrl}`" alt="Uploaded image" />
        </a>
        <small>Category: {{ post.category || "Uncategorized" }}
          | Author: {{ post.author || "Anonymous" }}
          | Date: {{ post.createdAt }} </small>
      </div>
    </div>
    <p v-else>No posts yet.</p>
  </div>
</template>


<style scoped>
.container {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  width: 100%;
  max-width: 800px;
  margin: 0 auto;
  padding: 20px;
}

/* body {
  background-color: #5e5e5e;
} */

h1 {
  color: #333;
  margin-bottom: 20px;
}

h2 {
  text-align: center;
  margin-bottom: 16px;
  color: #333;
}

.toggle-btn {
  padding: 10px 20px;
  /* background: #007bff; */
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  margin-bottom: 15px;
}

/* .toggle-btn:hover {
	background: #0056b3;
} */

/* Form */
.form-container {
  max-width: 500px;
  width: 100%;
  max-width: 500px;
  margin-bottom: 20px;
  padding: 20px;
  background: #fff;
  border-radius: 8px;
  box-shadow: 0px 4px 6px rgba(0, 0, 0, 0.1);
}

label {
  display: block;
  font-weight: bold;
  margin-bottom: 5px;
  color: #555;
}

input,
textarea {
  width: 100%;
  padding: 8px;
  margin-bottom: 12px;
  border: 1px solid #ccc;
  border-radius: 5px;
  font-size: 14px;
}

textarea {
  resize: vertical;
}

input:focus,
select:focus,
textarea:focus {
  border-color: #007bff;
  outline: none;
  box-shadow: 0 0 4px rgba(0, 123, 255, 0.5);
}

input[type="file"] {
  border: none;
  background: #f8f8f8;
  padding: 6px;
  cursor: pointer;
}

button {
  width: 100%;
  padding: 10px;
  background: #6b6b6b;
  color: white;
  border: none;
  border-radius: 5px;
  font-size: 16px;
  cursor: pointer;
  margin-top: 10px;
  transition: background 0.3s ease;
}

button:hover {
  background: #3b3b3b;
}

/* Posts */
.posts-container {
  width: 100%;
  max-width: 800px;
  margin-top: 20px;
}

.post {
  background: white;
  padding: 15px;
  border-radius: 8px;
  box-shadow: 0px 4px 6px rgba(0, 0, 0, 0.1);
  margin-bottom: 15px;
}

.post h3 {
  /* margin-bottom: 5px; */
  color: #333;
}

.post small {
  display: block;
  color: #777;
  margin-bottom: 10px;
}

/* Image */
.post img {
  width: 100%;
  max-width: 600px;
  border-radius: 5px;
  margin-top: 10px;
}
</style>
