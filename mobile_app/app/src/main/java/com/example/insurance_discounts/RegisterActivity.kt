package com.example.insurance_discounts

import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.example.insurance_discounts.clients.RegisterClient
import com.example.insurance_discounts.data.RegisterRequest
import com.example.insurance_discounts.databinding.ActivityRegisterBinding
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class RegisterActivity : AppCompatActivity() {
    private lateinit var binding: ActivityRegisterBinding
    private val registerClient = RegisterClient()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityRegisterBinding.inflate(layoutInflater)
        setContentView(binding.root)

        binding.loginLinkTextView.setOnClickListener {
            navigateToLoginActivity()
        }

        binding.registerButton.setOnClickListener {
            val email = binding.emailEditText.text.toString()
            println("email"+email.toString())
            val userName = binding.userNameEditText.text.toString()
            val password = binding.passwordEditText.text.toString()
            val confirmPassword = binding.confirmPasswordEditText.text.toString()
            val role = "User"

            if (validateFields(email, userName, password, confirmPassword)) {
                val registerRequest =
                    RegisterRequest(email, userName, password, confirmPassword, role)
                GlobalScope.launch(Dispatchers.Main) {
                    val response = withContext(Dispatchers.IO) {
                        registerClient.registerUser(registerRequest)
                    }

                    if (response.isSuccessful) {
                        Toast.makeText(
                            this@RegisterActivity,
                            "Registration successful",
                            Toast.LENGTH_SHORT
                        ).show()
                        navigateToMainActivity()
                    } else {
                        println(response)
                        Toast.makeText(
                            this@RegisterActivity,
                            "Registration failed",
                            Toast.LENGTH_SHORT
                        ).show()
                    }
                }
            } else {
                Toast.makeText(
                    this@RegisterActivity,
                    "Please fill in all fields",
                    Toast.LENGTH_SHORT
                ).show()
            }
        }
    }

    private fun validateFields(
        email: String,
        userName: String,
        password: String,
        confirmPassword: String
    ): Boolean {
        if (email.isEmpty() || userName.isEmpty() || password.isEmpty() || confirmPassword.isEmpty()) {
            return false
        }

        if (password != confirmPassword) {
            Toast.makeText(this, "Passwords do not match", Toast.LENGTH_SHORT).show()
            return false
        }

        return true
    }

    private fun navigateToMainActivity() {
        val intent = Intent(this, MainActivity::class.java)
        startActivity(intent)
        finish()
    }

    private fun navigateToLoginActivity() {
        val intent = Intent(this, MainActivity::class.java)
        startActivity(intent)
    }


}