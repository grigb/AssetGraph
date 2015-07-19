using UnityEngine;

using System;
using System.Linq;
using System.Collections.Generic;

namespace AssetGraph {
	public class FilterBase : INodeBase {
		public void Setup (string nodeId, string noUseLabel, List<InternalAssetData> inputSources, Action<string, string, List<InternalAssetData>> Output) {
			var absoluteSourcePaths = inputSources.Select(assetData => assetData.absoluteSourcePath).ToList();
			
			Action<string, List<string>> _PreOutput = (string label, List<string> outputSources) => {
				var outputs = new List<InternalAssetData>();
				foreach (var outputSource in outputSources) {
					foreach (var inputSource in inputSources) {
						if (outputSource == inputSource.absoluteSourcePath) {
							outputs.Add(inputSource);
						}
					}
				}

				Output(nodeId, label, outputs);
			};
			try {
				In(absoluteSourcePaths, _PreOutput);
			} catch (Exception e) {
				Debug.LogError("Filter:" + this + " error:" + e);
			}
		}
		
		public void Run (string nodeId, string noUseLabel, List<InternalAssetData> inputSources, Action<string, string, List<InternalAssetData>> Output) {
			var absoluteSourcePaths = inputSources.Select(assetData => assetData.absoluteSourcePath).ToList();
			
			Action<string, List<string>> _Output = (string label, List<string> outputSources) => {
				var outputs = new List<InternalAssetData>();
				foreach (var outputSource in outputSources) {
					foreach (var inputSource in inputSources) {
						if (outputSource == inputSource.absoluteSourcePath) {
							outputs.Add(inputSource);
						}
					}
				}

				Output(nodeId, label, outputs);
			};
			try {
				In(absoluteSourcePaths, _Output);
			} catch (Exception e) {
				Debug.LogError("Filter:" + this + " error:" + e);
			}
		}


		/**
			フィルタに対して自動的に呼ばれる関数。
		*/
		public virtual void In (List<string> source, Action<string, List<string>> Out) {
			Debug.LogError("should implement \"public override void In (List<string> source, Action<string, List<string>> Out)\" in class:" + this);
		}
	}
}