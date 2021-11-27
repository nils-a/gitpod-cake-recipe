# Docker image
This is an image for gitpod to base local development images off.

It contains 
* mono
* libgit2-dev

It does not contain any .NET components, since https://github.com/gitpod-io/gitpod/issues/6460

## sample
To use the bare metal image:

`.gitpod.yml`:

```yml
image: nilsa/cake-recipe-gitpod

tasks:
  - name: install .NET
    init: |+
      # install .NET as a workaround for https://github.com/gitpod-io/gitpod/issues/5090
      mkdir -p $DOTNET_ROOT
      wget https://dot.net/v1/dotnet-install.sh -O $DOTNET_ROOT/dotnet-install.sh
      chmod +x $DOTNET_ROOT/dotnet-install.sh
      dotnet-install.sh --channel 2.1 --install-dir $DOTNET_ROOT
      dotnet-install.sh --channel 3.1 --install-dir $DOTNET_ROOT
      dotnet-install.sh --channel 5.0 --install-dir $DOTNET_ROOT
      dotnet-install.sh --channel 6.0 --install-dir $DOTNET_ROOT

  - name: git unshallow and build
    command: |+
      git fetch --prune --unshallow
      ./build.sh

```
