# Kubernetes Troubleshooting User Guide - Image Pull Failure 

In the provided resource definition files, the selectors to focus on are `label` and `status.phase`. This guide will help in debugging and resolving issues pertaining to these selectors. 

## Overview 

- For the `label` selector with the key `app`, the current value is `image-pull-failure-pod`. 

- For the `field` selector, the key is empty but the path indicates `status.phase` and the current value is `Pending`.

## Problem Description

It appears that there's a pod with label app=image-pull-failure-pod that is stuck in `Pending` status. In Kubernetes, a common cause for a pod being stuck in `Pending`status is image pull failure.

> Note: The `Pending` phase implies that the Pod has been accepted by the Kubernetes system, but one or more of the Container images has not been created. This includes time before being scheduled as well as time spent downloading images - Kubernetes Official Documentation

## Solution steps

1. Verify that given pod is in `Pending` status.

```shell
kubectl get pods -l app=image-pull-failure-pod
```

If your pod is indeed in the `Pending` status, you can check the events associated with the Pod for more information. Use the command below with your specific Pod name.

```shell
kubectl describe pod <POD_NAME>
```

Here, you need to look out for `Error` or `Warning` events, especially ones related to `FailedPull` or `Failed` events. This will give information about why the pod is stuck in `Pending` phase.

2. Fixing the image pull error usually involves correcting the image name or pulling from the correct image registry.

Locate the portion of your deployed yaml where the image is defined. It is typically in the format below:

```yaml
containers:
- name: <CONTAINER_NAME>
  image: <WRONG_IMAGE_NAME>
```

You need to correct the image name. Replace `<WRONG_IMAGE_NAME>` with the valid image name and redeploy your yaml.

```shell
kubectl apply -f <YOUR_YAML_FILE>.yaml
```

After applying these changes your Pod should transit from the `Pending` phase to the `Running` phase.
