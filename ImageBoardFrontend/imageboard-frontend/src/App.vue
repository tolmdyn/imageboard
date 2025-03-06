<template>
	<div class="container">
		<h1>Image Board</h1>
		<router-view />
	</div>
</template>

<style scoped>
.container {
	display: flex;
	flex-direction: column;
	align-items: center;
	width: 100%;
	max-width: 800px;
	margin: 0 auto;
	padding: 20px;
}

h1 {
	color: #333;
	margin-bottom: 20px;
}
</style>