package com.example.insurance_discounts.data

data class UserInfo(
    val firstName: String,
    val lastName: String,
    val address: String,
    val birthdayDate: String,
    val discount: Float,
    val userName: String,
    val email: String
)