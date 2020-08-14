<template>
 <div>
   <b-row class="justify-content-md-center">
        <b-col col lg8="3"></b-col>
        <b-col cols="6">
          <b-form  @submit.stop.prevent>
            <label for="feedback-user">User ID</label>
            <b-input v-model="userId" :state="validation" id="feedback-user"></b-input>
            <b-form-invalid-feedback :state="validation">
              Your user ID must be 5-12 characters long.
            </b-form-invalid-feedback>
            <b-form-valid-feedback :state="validation">
              Looks Good.
            </b-form-valid-feedback>
            <label for="feedback-user">Password</label>
            <b-input v-model="userId" :state="validation" id="feedback-user"></b-input>
            <b-form-invalid-feedback :state="validation">
              Your user ID must be 5-12 characters long.
            </b-form-invalid-feedback>
            <b-form-valid-feedback :state="validation">
              Looks Good.
            </b-form-valid-feedback>
          </b-form>
      </b-col>
      <b-col col lg="3"></b-col>
   </b-row>
  </div>
</template>
<script>
  export default {
    data() {
      return {
        userId: ''
      }
    },
    computed: {
      validation() {
        return this.userId.length > 4 && this.userId.length < 13
      }
    }
  }
</script>
