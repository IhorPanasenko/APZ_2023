package com.example.insurance_discounts.data

data class RegisterRequest(
    val email: String,
    val userName: String,
    val password: String,
    val confirmPassword: String,
    val role: String
)