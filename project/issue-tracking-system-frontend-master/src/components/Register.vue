<template>
  <div>
    <b-form @submit.prevent="register">
      <b-form-group
        id="fieldset-userName"
        label="Enter your Name"
        label-for="input-userName"
        :invalid-feedback="invalidUserNameFeedback"
        :valid-feedback="validFeedback"
      >
        <b-form-input
          id="input-userName"
          v-model="userName"
          :state="userNameState"
          type="text"
          trim
          required
        ></b-form-input>
      </b-form-group>

      <b-form-group
        id="fieldset-userID"
        label="Enter your userID"
        label-for="input-userID"
        :invalid-feedback="invalidUserIDFeedback"
        :valid-feedback="validFeedback"
      >
        <b-form-input
          id="input-userID"
          v-model="userID"
          :state="userIDState"
          type="text"
          trim
          required
        ></b-form-input>
      </b-form-group>

      <b-form-group
        id="fieldset-password"
        label="Enter your Password"
        label-for="input-password"
        :invalid-feedback="invalidPasswordFeedback"
        :valid-feedback="validFeedback"
      >
        <b-form-input
          id="input-password"
          v-model="password"
          :state="passwordState"
          type="password"
          trim
          required
        ></b-form-input>
      </b-form-group>

      <b-form-group
        id="fieldset-email"
        label="Enter your Email"
        label-for="input-email"
        :invalid-feedback="invalidEmailFeedback"
        :valid-feedback="validFeedback"
        :state="emailState"
      >
        <b-form-input
          id="input-email"
          v-model="email"
          :state="emailState"
          type="email"
          trim
          required
        ></b-form-input>
      </b-form-group>
      <b-button type="submit" :disabled="!btnLoginEnabled">Sign up</b-button>
    </b-form>
    <b-modal id="Alertmodal" ref="AlertModal" ok-only centered>
      <b-alert class="left" show variant="danger">{{ errorMessage }}</b-alert>
    </b-modal>
  </div>
</template>


<script>
import axios from "axios";
import { async } from "q";

export default {
  data() {
    return {
      userID: "",
      userName: "",
      password: "",
      email: "",
      isPasswordOrUserNameError: false,
      errorMessage: "an existing item already exists"
    };
  },
  components: {},
  methods: {
    async register() {
      //write login authencation logic here!
      let data = {
        account: this.userID,
        password: this.password,
        name: this.userName,
        email: this.email,
        charactorId: 2
      };
      let res = null;
      res = await axios
        .post(`${process.env.VUE_APP_APIPATH}/api/user`, data, {
          headers: { "content-type": "application/json;charset=utf-8" }
        })
        .then(async function(res) {
          return res;
        })
        .catch(async function(err) {
          return err.response;
        });
      if (res.status >= 200 && res.status < 300) {
        console.log(res.data);
        this.$emit("registerSuccess");
      } else if (res.status == 400) {
        this.errorMessage = res.data;
        this.isPasswordOrUserIDError = true;
        this.$refs.AlertModal.show();
      } else if (res.status == 409) {
        this.errorMessage = res.data;
        this.isPasswordOrUserIDError = true;
        this.$refs.AlertModal.show();
      }
    }
  },
  computed: {
    btnLoginEnabled() {
      return this.userNameState && this.passwordState && this.userIDState;
    },
    userNameState() {
      return this.userName.length > 0;
    },
    passwordState() {
      return this.password.length >= 4;
    },
    userIDState() {
      return this.userID.length >= 4;
    },
    emailState() {
      return this.email.length > 0;
    },
    invalidUserNameFeedback() {
      if (this.userName.length > 0) {
        return "";
      } else {
        return "Please enter something";
      }
    },
    invalidUserIDFeedback() {
      if (this.userID.length > 0) {
        return "";
      } else if (this.userID.length > 0) {
        return "Enter at least 4 characters";
      } else {
        return "Please enter something";
      }
    },
    invalidEmailFeedback() {
      if (this.email.length > 4) {
        return "";
      } else if (this.email.length > 0) {
        return "Enter at least 4 characters";
      } else {
        return "Please enter something";
      }
    },
    invalidPasswordFeedback() {
      if (this.password.length > 4) {
        return "";
      } else if (this.userID.length > 0) {
        return "Enter at least 4 characters";
      } else {
        return "Please enter something";
      }
    },
    validFeedback() {
      return "";
    }
  }
};
</script>
