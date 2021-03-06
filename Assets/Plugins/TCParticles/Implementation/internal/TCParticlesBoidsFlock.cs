using System;
using UnityEngine;

namespace TC.Internal
{
	public class TCParticlesBoidsFlock
	{
		private ComputeBuffer m_posSumBuffer;
		private ComputeBuffer m_velocitySumBuffer;

		private ComputeBuffer m_tempPosBuffer;
		private ComputeBuffer m_tempVelocityBuffer;

		private int m_sumKernel;
		private int m_updateBoidsKernel;
		private int m_initKernel;

		private float m_number;

		private ParticleManager m_system;
		private ParticleForceManager m_force;
		private ComputeShader m_computeShader;


		public TCParticlesBoidsFlock(ParticleManager system, ParticleForceManager force, ComputeShader computeShader) {
			m_system = system;
			m_computeShader = computeShader;
			m_force = force;

			m_posSumBuffer = new ComputeBuffer(system.MaxParticles, 12);
			m_velocitySumBuffer = new ComputeBuffer(system.MaxParticles / 32, 12);

			m_tempPosBuffer = new ComputeBuffer(system.MaxParticles, 12);
			m_tempVelocityBuffer = new ComputeBuffer(system.MaxParticles / 32, 12);

			m_sumKernel = computeShader.FindKernel("BoidsFlockSum");
			m_initKernel = computeShader.FindKernel("BoidsFlockInit");

			m_updateBoidsKernel = computeShader.FindKernel("BoidsFlockUpdate");
		}

		private void Sum(ComputeBuffer i, ComputeBuffer o, float groups) {
			m_computeShader.SetBuffer(m_sumKernel, "sumInput", i);
			m_computeShader.SetBuffer(m_sumKernel, "sumOutput", o);

			m_computeShader.Dispatch(m_sumKernel, Mathf.RoundToInt(groups), 1, 1);
		}

		private void SumRoutine(ComputeBuffer buf1, ComputeBuffer buf2, string setBuffer) {
			int nsum = Mathf.FloorToInt(m_system.ParticleCount / 2.0f);
			float groups = nsum / 16.0f;
			int rest = Mathf.RoundToInt(groups) % 32;
			bool flip = false;


			while (groups > 0.0f) {
				if (flip) {
					Sum(buf2, buf1, groups);
				}
				else {
					Sum(buf1, buf2, groups);
				}

				flip = !flip;

				groups /= 32.0f;
			}

			m_computeShader.SetBuffer(m_updateBoidsKernel, setBuffer, flip ? buf1 : buf2);
			m_computeShader.SetInt("rest", rest);
		}


		public void UpdateBoids() {
			m_computeShader.SetInt("n", m_system.ParticleCount);
			m_computeShader.SetFloat("nDiv", 1.0f / m_system.ParticleCount);

			m_computeShader.SetFloat("boidsPosStr", m_force.boidsPositionStrength);
			m_computeShader.SetFloat("boidsVelStr", m_force.boidsVelocityStrength);


			m_computeShader.SetBuffer(m_initKernel, "averagePos", m_posSumBuffer);
			m_computeShader.SetBuffer(m_initKernel, "averageVelocity", m_velocitySumBuffer);
			m_computeShader.Dispatch(m_initKernel, m_system.DispatchCount, 1, 1);


			SumRoutine(m_posSumBuffer, m_tempPosBuffer, "averagePos");
			SumRoutine(m_velocitySumBuffer, m_tempVelocityBuffer, "averageVelocity");

			m_computeShader.SetVector("boidsCenter", m_system.Transform.position);
			m_computeShader.SetFloat("boidsCenterStr", m_force.boidsCenterStrength);

			m_computeShader.Dispatch(m_updateBoidsKernel, m_system.DispatchCount, 1, 1);
		}

		public void ReleaseBuffers() {
			if (m_posSumBuffer != null) {
				m_posSumBuffer.Release();
			}

			if (m_velocitySumBuffer != null) {
				m_velocitySumBuffer.Release();
			}

			if (m_tempPosBuffer != null) {
				m_tempPosBuffer.Release();
			}

			if (m_tempVelocityBuffer != null) {
				m_tempVelocityBuffer.Release();
			}
		}
	}
}