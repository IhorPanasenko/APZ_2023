package com.example.insurance_discounts.adapters

import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.example.insurance_discounts.data.Agent
import com.example.insurance_discounts.databinding.ItemInsuranceAgentBinding

class AgentsAdapter() : RecyclerView.Adapter<AgentsAdapter.AgentViewHolder>() {
    private val agents: MutableList<Agent> = mutableListOf()

    fun setAgents(agents: List<Agent>) {
        this.agents.clear()
        this.agents.addAll(agents)
        notifyDataSetChanged()
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): AgentViewHolder {
        val inflater = LayoutInflater.from(parent.context)
        val binding = ItemInsuranceAgentBinding.inflate(inflater, parent, false)
        return AgentViewHolder(binding)
    }

    override fun onBindViewHolder(holder: AgentViewHolder, position: Int) {
        val agent = agents[position]
        holder.bind(agent)
    }

    override fun getItemCount(): Int {
        return agents.size
    }

    inner class AgentViewHolder(private val binding: ItemInsuranceAgentBinding) :
        RecyclerView.ViewHolder(binding.root) {

        fun bind(agent: Agent) {
            binding.nameTextView.text = "${agent.firstName} ${agent.secondName}"
            binding.companyNameTextView.text = agent.company.companyName
            binding.phoneNumberTextView.text = agent.phoneNumber
            binding.emailTextView.text = agent.emailAddress
            binding.ratingBar.rating = agent.raiting
        }
    }
}